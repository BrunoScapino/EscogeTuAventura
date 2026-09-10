using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;

namespace DTOs.DTOs
{
    public class HistoriaDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Sinopsis { get; set; } = string.Empty;

        public EstadoHistoria Estado { get; set; }
    }
}