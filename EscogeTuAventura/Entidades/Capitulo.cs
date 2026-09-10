using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;

namespace EscogeTuAventura.Entidades
{
    public abstract class Capitulo : IValidable
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Texto { get; set; } = string.Empty;

        public Historia Historia { get; set; } = null!;

        public abstract void Validar();
    }
}
