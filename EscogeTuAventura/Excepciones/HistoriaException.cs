using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeTuAventura.Excepciones
{
    public class HistoriaException : Exception
    {
        public HistoriaException() { }

        public HistoriaException(string message) : base(message) { }

        public HistoriaException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}