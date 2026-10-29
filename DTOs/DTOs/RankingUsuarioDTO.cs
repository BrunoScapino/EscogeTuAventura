using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class RankingUsuarioDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;

        public int CantidadHistoriasCompletadas { get; set; }
    }
}