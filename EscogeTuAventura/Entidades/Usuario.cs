using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using EscogeTuAventura.ValueObjects.Usuario;
using EscogeTuAventura.Excepciones;
using System.ComponentModel.DataAnnotations;


namespace EscogeTuAventura.Entidades
{
    public class Usuario : IValidable
    {
        public int Id { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;

        public Password Password { get; set; } = null!;

        public Rol Rol { get; set; }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(NombreCompleto))
                throw new UsuarioException("El nombre completo es requerido.");

            if (string.IsNullOrWhiteSpace(Email))
                throw new UsuarioException("El email es requerido.");

            if (!new EmailAddressAttribute().IsValid(Email))
                throw new UsuarioException("El email no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(NombreUsuario))
                throw new UsuarioException("El nombre de usuario es requerido.");

            if (!Enum.IsDefined(typeof(Rol), Rol))
                throw new UsuarioException("El rol indicado no es válido.");

            if (Password == null)
                throw new UsuarioException("La contraseña es requerida.");

            Password.Validar();
        }
    }
}