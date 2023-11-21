﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;
using WebMedicina.Shared.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMedicina.BackEnd.Dal {
    public  class AdminDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminDal(WebmedicinaContext context, IMapper mapper, UserManager<IdentityUser> userManager) {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        public bool CrearNuevoMedico (UserRegistroDto nuevoMedico, string idUsuario) {
            try {
                MedicosModel medicosModel = _mapper.Map<MedicosModel>(nuevoMedico);
                medicosModel.NetuserId = idUsuario;

                _context.Medicos.Add(medicosModel);
                // La operación se realizó correctamente
                return true; 
            } catch (Exception) {
                return false;   
            }
        }
        public async Task<IdentityUser?> ObtenerUsuarioIdentity(string userName) {
            try {
                return await _userManager.FindByNameAsync(userName);
            } catch (Exception) {
                throw;
            }
        }

        public async Task<string?> ObtenerRolUser(IdentityUser? user) {
            try {
  
                IList<string>? roles = null;

                if (user is not null) {
                    roles = await _userManager.GetRolesAsync(user);
                }

                return roles?.FirstOrDefault().ToString();
            } catch (Exception) {
                throw;
            }
        }
        public async Task<List<UserUploadDto>> ObtenerMedicos(Dictionary<string, string> filtros, UserInfoDto admin) {
            try { 
                List<UserUploadDto> listaMedicos = new();

                // Validamos si hay que filtrar por rol o no
                if (string.IsNullOrEmpty(filtros["rol"])) {


                    // Obtenemos los usuarios con los filtros seleccionados
                    listaMedicos = (from u in _context.Medicos
                                                    where (u.IdMedico != admin.IdMedico &&
                                                    (!string.IsNullOrEmpty(filtros["busqueda"]) || (u.UserLogin == filtros["busqueda"] || u.Nombre.StartsWith(filtros["busqueda"])
                                   || u.Apellidos.Contains(filtros["busqueda"]) || u.Apellidos.StartsWith(filtros["busqueda"]))))
                                   select _mapper.Map<UserUploadDto>(u)).ToList();

                    // Mapeamos los medicos y obtenemos su rol
                    foreach (UserUploadDto usuario in listaMedicos)
                    {
                        var role = await ObtenerRolUser(await ObtenerUsuarioIdentity(usuario.UserLogin));
                        if (role != null) {
                            usuario.Rol = role;
                        }
                    }
                } else {
                    
                    // Obtenemos todos los usuarios con el rol indicado y generamos una lista
                    var usuariosConRol = await _userManager.GetUsersInRoleAsync(filtros["rol"]);
                    IEnumerable<string> listaUserNames =  (from q in usuariosConRol where q.NormalizedUserName is not null select q.NormalizedUserName).ToArray();


                    //Obtenemos los usuarios con los filtros seleccionados
                    listaMedicos = (from u in _context.Medicos
                                    where (u.IdMedico != admin.IdMedico && 
                        (listaUserNames.Contains(u.UserLogin) || (string.IsNullOrEmpty(filtros["busqueda"]) || (u.UserLogin == filtros["busqueda"] || u.Nombre.StartsWith(filtros["busqueda"])
                        || u.Apellidos.Contains(filtros["busqueda"]) || u.Apellidos.StartsWith(filtros["busqueda"])))))
                                    select _mapper.Map<UserUploadDto>(u)).ToList();

                    // Añadimos el rol a los usuarios
                    listaMedicos.ForEach(m => m.Rol = filtros["rol"]);
                }

                return listaMedicos;
            } catch (Exception) {
                    throw;
            }
        }

        // Update del medico con el numHistoria especificado
        public async Task<bool> UpdateMedico (UserUploadDto medicoActualizado) {
            try {

                // Obtenemos el medico
                MedicosModel? medico = _context.Medicos.Find(medicoActualizado.IdMedico);

                // Actualizamos las propiedades
                if(medico != null) {
                    medico.Nombre = medicoActualizado.Nombre;
                    medico.Apellidos = medicoActualizado.Apellidos;
                    medico.FechaNac = medicoActualizado.FechaNac ?? medico.FechaNac;
                    medico.Sexo = medicoActualizado.Sexo;
                    return await _context.SaveChangesAsync() > 0;
                }
                return false;
            } catch (Exception) {
                throw;
            }
        }
    }
}
