using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using EscogeTuAventura.Excepciones;


namespace EscogeTuAventura.Entidades
{
    public class Categoria : IValidable
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
                throw new CategoriaException("El nombre de la categoría es requerido.");
        }
    }
}