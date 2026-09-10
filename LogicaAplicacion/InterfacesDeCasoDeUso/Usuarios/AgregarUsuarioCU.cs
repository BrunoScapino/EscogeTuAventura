using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;

namespace LogicaAplicacion.CasosDeUso.Usuarios
{
    public class AgregarUsuarioCU : IAgregarUsuarioCU
    {
        private IRepositorioUsuarios repositorioUsuarios;

        public AgregarUsuarioCU(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public void Agregar(UsuarioDTO usuario)
        {
            Usuario nuevoUsuario = UsuarioMapper.FromDTO(usuario);
            repositorioUsuarios.Add(nuevoUsuario);
        }
    }
}