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
    public class ObtenerUsuarioPorIdCU : IObtenerUsuarioPorIdCU
    {
        private IRepositorioUsuarios repositorioUsuarios;

        public ObtenerUsuarioPorIdCU(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public UsuarioDTO ObtenerPorId(int id)
        {
            Usuario usuario = repositorioUsuarios.FindById(id);

            if (usuario == null)
            {
                return null;
            }

            return UsuarioMapper.ToDTO(usuario);
        }
    }
}