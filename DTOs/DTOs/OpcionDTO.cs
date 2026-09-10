using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class OpcionDTO
    {
        public int Id { get; set; }

        public string Texto { get; set; } = string.Empty;

        public int CapituloDestinoId { get; set; }
    }
}