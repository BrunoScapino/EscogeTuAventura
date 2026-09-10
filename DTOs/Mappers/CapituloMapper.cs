using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public class CapituloMapper
    {
        public static CapituloDTO ToDTO(Capitulo capitulo)
        {
            return new CapituloDTO()
            {
                Id = capitulo.Id,
                Titulo = capitulo.Titulo,
                Texto = capitulo.Texto
            };
        }
    }
}