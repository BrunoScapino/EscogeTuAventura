using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using EscogeTuAventura.Excepciones;


namespace EscogeTuAventura.Entidades
{
    public class Opcion : IValidable
    {
        public int Id { get; set; }

        public string Texto { get; set; } = string.Empty;

        public CapituloIntermedio CapituloOrigen { get; set; } = null!;

        public Capitulo CapituloDestino { get; set; } = null!;

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Texto))
                throw new OpcionException("El texto de la opción es requerido.");

            if (CapituloOrigen == null)
                throw new OpcionException("La opción debe tener un capítulo origen.");

            if (CapituloDestino == null)
                throw new OpcionException("La opción debe tener un capítulo destino.");
        }
    }
}