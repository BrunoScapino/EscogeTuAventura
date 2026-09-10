using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using EscogeTuAventura.Excepciones;

using System;

namespace EscogeTuAventura.Entidades
{
    public class Lectura : IValidable
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public Historia Historia { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        public CapituloFinal? FinalAlcanzado { get; set; }

        public void Validar()
        {
            if (Usuario == null)
                throw new LecturaException("La lectura debe tener un usuario.");

            if (Historia == null)
                throw new LecturaException("La lectura debe tener una historia.");

            if (FechaFinalizacion.HasValue && FechaFinalizacion.Value < FechaInicio)
                throw new LecturaException("La fecha de finalización no puede ser anterior a la fecha de inicio.");
        }
    }
}