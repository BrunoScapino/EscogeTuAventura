using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }

        public string Accion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public int AdministradorId { get; set; }

        public int HistoriaId { get; set; }
    }
}