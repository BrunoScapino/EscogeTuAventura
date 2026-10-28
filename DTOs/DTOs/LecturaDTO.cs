using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class LecturaDTO
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int HistoriaId { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        public int? CapituloFinalId { get; set; }

        public string HistoriaTitulo { get; set; } = string.Empty;

        public string? CapituloFinalTitulo { get; set; }
    }
}