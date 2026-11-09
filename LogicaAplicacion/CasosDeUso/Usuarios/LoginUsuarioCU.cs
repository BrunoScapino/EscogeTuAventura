using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;

namespace LogicaAplicacion.CasosDeUso.Usuarios
{
    public class LoginUsuarioCU : ILoginUsuarioCU
    {
        private IRepositorioUsuarios repositorioUsuarios;

        public LoginUsuarioCU(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public UsuarioDTO Login(LoginDTO login)
        {
            if (login == null)
            {
                throw new UsuarioException("Los datos de login son requeridos.");
            }

            var usuario = repositorioUsuarios.Login(login.NombreUsuario, login.Password);

            return UsuarioMapper.ToDTO(usuario);
        }
    }
}