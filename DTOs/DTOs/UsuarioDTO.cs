using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;

namespace DTOs.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Rol Rol { get; set; }
    }
}