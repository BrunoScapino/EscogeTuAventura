using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class CapituloDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Texto { get; set; } = string.Empty;

        public int HistoriaId { get; set; }

        public int CapituloAnteriorId { get; set; }

        public bool EsFinal { get; set; }

        public string TextoOpcion { get; set; } = string.Empty;
    }
}