using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeTuAventura.Excepciones
{
    public class OpcionException : Exception
    {
        public OpcionException() { }

        public OpcionException(string message) : base(message) { }

        public OpcionException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}