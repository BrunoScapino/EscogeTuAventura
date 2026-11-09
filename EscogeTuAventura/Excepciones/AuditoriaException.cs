using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeTuAventura.Excepciones
{
    public class AuditoriaException : Exception
    {
        public AuditoriaException() { }

        public AuditoriaException(string message) : base(message) { }

        public AuditoriaException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}