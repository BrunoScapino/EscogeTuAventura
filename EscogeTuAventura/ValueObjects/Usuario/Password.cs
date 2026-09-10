using System;
using System.Linq;

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
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La contraseña es requerida.");

            if (value.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

            if (!value.Any(char.IsUpper))
                throw new ArgumentException("La contraseña debe contener al menos una mayúscula.");

            if (!value.Any(char.IsLower))
                throw new ArgumentException("La contraseña debe contener al menos una minúscula.");

            if (!value.Any(char.IsDigit))
                throw new ArgumentException("La contraseña debe contener al menos un número.");

            if (!value.Any(c => !char.IsLetterOrDigit(c)))
                throw new ArgumentException("La contraseña debe contener al menos un carácter especial.");

            Value = value;
        }

        public bool Verify(string value)
        {
            return Value == value;
        }

        public override string ToString()
        {
            return "********";
        }
    }
}