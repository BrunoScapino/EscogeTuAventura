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
    public class EncontrarTodosUsuariosCU : IEncontrarTodosUsuariosCU
    {
        private IRepositorioUsuarios repositorioUsuarios;

        public EncontrarTodosUsuariosCU(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }
        public List<UsuarioDTO> EncontrarTodos()
        {
            return repositorioUsuarios.FindAll()
                .Select(usuario => UsuarioMapper.ToDTO(usuario))
                .ToList();
        }
    }
    
}