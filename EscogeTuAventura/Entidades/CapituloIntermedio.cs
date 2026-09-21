using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using System.Collections.Generic;
using EscogeTuAventura.Excepciones;

namespace EscogeTuAventura.Entidades
{
    public class CapituloIntermedio : Capitulo
    {
        public List<Opcion> Opciones { get; set; } = new List<Opcion>();

        public override void Validar()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                throw new CapituloException("El título del capítulo es requerido.");

            if (string.IsNullOrWhiteSpace(Texto))
                throw new CapituloException("El texto del capítulo es requerido.");
        }
    }
}