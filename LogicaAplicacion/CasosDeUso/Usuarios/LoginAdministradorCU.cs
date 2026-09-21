using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;

namespace LogicaAplicacion.CasosDeUso.Usuarios
{
    public class LoginAdministradorCU : ILoginAdministradorCU
    {
        private IRepositorioUsuarios repositorioUsuarios;

        public LoginAdministradorCU(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public UsuarioDTO Login(string nombreUsuario, string password)
        {
            Usuario usuario = repositorioUsuarios.Login(nombreUsuario, password);

            if (usuario == null)
                throw new UsuarioException("Usuario o contraseña incorrectos.");

            if (usuario.Rol != Rol.Administrador)
                throw new UsuarioException("El usuario no tiene permisos de administrador.");

            return UsuarioMapper.ToDTO(usuario);
        }
    }
}