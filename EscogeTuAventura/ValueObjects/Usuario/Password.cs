using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeTuAventura.ValueObjects.Usuario
{
    public sealed record Password
    {
        public string Value { get; init; }

        public Password(string plaintext)
        {
            if (string.IsNullOrWhiteSpace(plaintext))
                throw new ArgumentException(
                    "Password requerido",
                    nameof(plaintext));

            if (plaintext.Length < 8)
                throw new ArgumentException(
                    "Password debe tener al menos 8 caracteres",
                    nameof(plaintext));

            bool hasLower = false;
            bool hasUpper = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (var ch in plaintext)
            {
                if (char.IsLower(ch))
                    hasLower = true;
                else if (char.IsUpper(ch))
                    hasUpper = true;
                else if (char.IsDigit(ch))
                    hasDigit = true;
                else
                    hasSpecial = true;

                if (hasLower && hasUpper && hasDigit && hasSpecial)
                    break;
            }

            if (!(hasLower && hasUpper && hasDigit && hasSpecial))
                throw new ArgumentException(
                    "Password debe contener al menos una minúscula, una mayúscula, un número y un carácter especial",
                    nameof(plaintext));

            Value = plaintext;
        }

        public bool Verify(string plaintext)
        {
            return Value == plaintext;
        }

        public override string ToString()
        {
            return "••••••••";
        }
    }
}