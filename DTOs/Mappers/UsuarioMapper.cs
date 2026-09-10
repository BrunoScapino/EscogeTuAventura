using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.ValueObjects.Usuario;

namespace DTOs.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario FromDTO(UsuarioDTO dto)
        {
            return new Usuario
            {
                Id = dto.Id,
                NombreCompleto = dto.NombreCompleto,
                Email = dto.Email,
                NombreUsuario = dto.NombreUsuario,
                Password = new Password(dto.Password),
                Rol = dto.Rol
            };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO()
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario,
                Password = usuario.Password.Value,
                Rol = usuario.Rol
            };
        }
    }
}