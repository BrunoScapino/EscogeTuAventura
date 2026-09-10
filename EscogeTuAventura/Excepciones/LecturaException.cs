using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeTuAventura.Excepciones
{
    public class LecturaException : Exception
    {
        public LecturaException() { }

        public LecturaException(string message) : base(message) { }

        public LecturaException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}