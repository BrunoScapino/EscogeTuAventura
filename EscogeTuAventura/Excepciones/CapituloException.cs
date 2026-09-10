using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeTuAventura.Excepciones
{
    public class CapituloException : Exception
    {
        public CapituloException() { }

        public CapituloException(string message) : base(message) { }

        public CapituloException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}