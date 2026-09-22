using System;
using System.Linq;
using EscogeTuAventura.Excepciones;

namespace EscogeTuAventura.ValueObjects.Usuario
{
    public sealed record Password
    {
        public string Value { get; init; }

        private Password()
        {
            Value = string.Empty;
        }

        public Password(string value)
        {
            Value = value;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new UsuarioException("La contraseña es requerida.");

            if (Value.Length < 8)
                throw new UsuarioException(
                    "La contraseña debe tener al menos 8 caracteres.");

            if (!Value.Any(char.IsUpper))
                throw new UsuarioException(
                    "La contraseña debe contener al menos una mayúscula.");

            if (!Value.Any(char.IsLower))
                throw new UsuarioException(
                    "La contraseña debe contener al menos una minúscula.");

            if (!Value.Any(char.IsDigit))
                throw new UsuarioException(
                    "La contraseña debe contener al menos un número.");

            if (!Value.Any(c =>
                !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                throw new UsuarioException(
                    "La contraseña debe contener al menos un carácter especial.");
            }
        }

        public bool Verify(string value)
        {
            return string.Equals(Value, value, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return "********";
        }
    }
}