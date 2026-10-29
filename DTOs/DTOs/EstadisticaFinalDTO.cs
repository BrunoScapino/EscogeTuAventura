using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class EstadisticaFinalDTO
    {
        public int IdHistoria { get; set; }

        public string TituloHistoria { get; set; } = string.Empty;

        public int IdFinal { get; set; }

        public string TituloFinal { get; set; } = string.Empty;

        public int VecesAlcanzado { get; set; }

        public bool EsFinalMasAlcanzado { get; set; }
    }
}