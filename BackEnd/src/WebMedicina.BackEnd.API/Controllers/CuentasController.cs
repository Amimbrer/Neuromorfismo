﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("/api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;
        IdentityContext _identityContext;
        WebmedicinaContext _context;

        // Contructor con inyeccion de dependencias
        public CuentasController(IConfiguration configuration, IMapper mapper, IAdminsService adminService, IIdentityService identityService,
            IdentityContext identityContext, WebmedicinaContext context) {
            _configuration = configuration;
            _mapper = mapper;
            _adminService = adminService;
            _identityService = identityService;
            _identityContext = identityContext;
            _context = context;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserRegistroDto model) {
            using (var transactionIdentity = _identityContext.Database.BeginTransaction()) {
                try {
                    if (ModelState.IsValid && model != null) {
                        var user = new IdentityUser {
                            UserName = model.NumHistoria
                        };

                        // Creamos user con identity
                        if (await _identityService.CrearUser(user, model)) {

                            UserInfoDto userInfo = _mapper.Map<UserInfoDto>(model);

                            // Insertamos el nuevo medico a la tabla y generamos token si todo esta OK
                            if (_adminService.CrearMedico(model, user.Id)) {
                                await transactionIdentity.CommitAsync();
                                _context.SaveChanges();
                                return BuildToken(userInfo);
                            }
                            // Revertimos toda la transacción si el usuario no se ha creado correctamente
                            await transactionIdentity.RollbackAsync();
                            return BadRequest("Ha surgido un error al crear el nuevo usuario");
                        } else {
                            return BadRequest($"Ya existe un usuario con el username: {model.NumHistoria}");
                        }

                    } else {
                        return BadRequest("Alguno de los campos no es valido");
                    }
                } catch (Exception) {
                    await transactionIdentity.RollbackAsync();
                    return StatusCode(500, "Error interno del servidor");
                }
            }
        }


            [HttpPost("login")]
            public async Task<ActionResult<UserToken>> Login([FromBody] UserLoginDto userLogin) {
                try {
                    if (ModelState.IsValid) {
                        if (await _identityService.ComprobarContraseña(userLogin)) {

                        // Obtenemos los datos del medico y su rol
                        MedicosModel? medico = await _identityService.ObtenerUsuarioYRol(userLogin.UserName);

                            // Generamos la info del usuario si se ha obtenido correctamente
                            UserInfoDto userInfo = new();
                            if(medico is not null) {
                                userInfo = _mapper.Map<UserInfoDto>(medico);
                                //userInfo.Rol = medico.Role;
                            }
                                return BuildToken(userInfo);

                        } else {
                            ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
                            return BadRequest(ModelState);
                        }
                    } else {
                        return BadRequest(ModelState);
                    }
                } catch (Exception ex) {
                    return StatusCode(500, "Error interno del servidor");
                }
            }

            [HttpPost("crearRol")]
            public async Task<IActionResult> CrearRol([FromBody] string nombreRol) {

                //var roleExists = await _roleManager.RoleExistsAsync(nombreRol);
                //if (!roleExists) {
                //    // Añadir el nuevo rol
                //    var identityRole = new IdentityRole { Name = nombreRol };
                //    //var result =  await _roleManager.CreateAsync(identityRole);
                //     //if (result.Succeeded) {
                //        return Ok("Rol creado exitosamente");
                //    //} 

                //}
                return BadRequest("Error al crear el rol");

            }




            private UserToken BuildToken(UserInfoDto userInfo) {
                var claims = new List<Claim>();
                {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.NumHistoria);
                new Claim(ClaimTypes.Name, userInfo.Nombre);
                new Claim(ClaimTypes.Surname, userInfo.Apellidos);
                new Claim(ClaimTypes.DateOfBirth, userInfo.FechaNac.ToString("dd/MM/yyyy"));
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            };

            // recorremos los roles del usuario y los añadimos
            if(userInfo.Roles is not null) {
                foreach (var role in userInfo.Roles) {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
                var expiration = DateTime.UtcNow.AddDays(7);

                JwtSecurityToken token = new JwtSecurityToken(
                   issuer: null,
                   audience: null,
                   claims: claims,
                   expires: expiration,
                   signingCredentials: creds);

                return new UserToken() {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            }

        }
    }
