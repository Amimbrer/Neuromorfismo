using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Neuromorfismo.BackEnd.Dto;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class CuentasController : ControllerBase {
        private readonly IUserAccountService _userAccountService;
        private readonly IIdentityService _identityService;

        // Contructor con inyeccion de dependencias
        public CuentasController(IUserAccountService userAccountService, IIdentityService identityService) {
            _userAccountService = userAccountService;
            _identityService = identityService;
        }

        [HttpPost("crear")]
        public async Task<ActionResult> CreateUser([FromBody] UserRegistroDto model) {
            try {
                EstadoCrearUsuario estadoCrearUsu = EstadoCrearUsuario.ModeloKO;

                if (ModelState.IsValid) {
                    estadoCrearUsu = await _userAccountService.CrearUsuarioYMedico(model);
                }


                switch (estadoCrearUsu) {
                    case EstadoCrearUsuario.MedicoKO:
                    return BadRequest("No ha podido crearse el nuevo médico, inténtelo más tarde");
                    case EstadoCrearUsuario.IdentityUserKO:
                    return BadRequest($"Ya existe un usuario con el username: {model?.UserLogin}, o el username no es válido");
                    case EstadoCrearUsuario.UserYMedicoOK:
                    // Devolvemos que la respuesta ha sido correcta
                    return Ok($"Nuevo {model?.Rol} creado correctamente");
                    default:
                    return BadRequest("Datos del nuevo usuario incorrectos");
                }

            } catch (Exception) {
                return BadRequest("Datos del nuevo usuario incorrectos");
            }
        }


        [HttpPost("autenticarusuario")]
        [AllowAnonymous]
        public async Task<ActionResult<Tokens>> AutenticarUsuario([FromBody] UserLoginDto userLogin) {
            try {
                // Comprobamos si la contraseña es válida
                if (ModelState.IsValid && await _identityService.ComprobarContraseña(userLogin)) {

                    Tokens? tokens = _userAccountService.ObtenerTokenLogin(userLogin);

                    // Devolvemos token y el refreshToken si han sido generados correctamente
                    if (tokens is not null) {
                        return Ok(tokens);
                    }
                }

                return Unauthorized();
            } catch (Exception) {
                return BadRequest();
            }
        }

        // Generamos y comprobamos si el userName está disponible 
        [HttpPost("generarusername")]
        public async Task<ActionResult> GenerarUserName([FromBody] string[] nomYApell) {
            try {
                var respuesta = await _identityService.GenerarUserName(nomYApell);
                if (respuesta.userNameInvalido == false) {
                    return Ok(respuesta.userNameGenerado);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        // Cerrar sesion de un usuario
        [HttpPost("cerrarsesion")]
        [Authorize]
        public void CerrarSesion([FromBody] Tokens tokens) {
            if (ModelState.IsValid) {
                _userAccountService.CerrarSesion(tokens, User.ToUserInfoDto());
            }
        }

        // Validar token de autenticacion y refrescarlo si está caducado
        [HttpPost("autenticarportoken")]
        [AllowAnonymous]
        public AutenticarPorTokenDto? AutenticarPorToken ([FromBody] Tokens? tokens) {
            AutenticarPorTokenDto? respuesta = null;

            // Validamos si el token está caducado
            if (tokens is not null && !string.IsNullOrWhiteSpace(tokens.AccessToken) && !string.IsNullOrWhiteSpace(tokens.RefreshToken)) {

                JwtSecurityToken jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(tokens.AccessToken);

                // Si el token está caducado lo refrescamos
                if (jsonToken.ValidTo < DateTime.Now) {

                    // Se devolverá null si el token no ha podido ser autenticado
                    tokens = _userAccountService.RefreshAccesToken(tokens);
                    respuesta = new(true, tokens);
                } else {
                    respuesta = new(false, tokens);
                }
            } 

            return respuesta;
        }

        /// <summary>
        /// Permite a un usuario cambiar la contraseá especificando la contraseña antigua y la nueva
        /// </summary>
        /// <param name="contrasenas"></param>
        /// <returns></returns>
        [HttpPatch("cambiarpassword")]
        [Authorize]
        public async Task<ActionResult<CodigosErrorChangePass[]>> CambiarContrasena([FromBody] ChangePasswordDto contrasenas) {
            return Ok(await _userAccountService.CambiarContrasena(contrasenas, User.ToUserInfoDto()));
        }


        /// <summary>
        /// Permite reestablecer la contraseña con el user y la nueva password a un admin
        /// </summary>
        /// <param name="resetPass"></param>
        /// <returns></returns>
        [HttpPatch("restartpass")]
        public async Task<ActionResult<bool>> ReestablacerPass([FromBody] RestartPasswordDto resetPass) {
           return Ok(await _identityService.RestablecerPassword(resetPass));
        }
    }
}
