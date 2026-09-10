using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public static class OpcionMapper
    {
        public static OpcionDTO ToDTO(Opcion opcion)
        {
            return new OpcionDTO
            {
                Id = opcion.Id,
                Texto = opcion.Texto,
                CapituloDestinoId = opcion.CapituloDestino.Id
            };
        }

        public static Opcion ToEntity(OpcionDTO dto)
        {
            return new Opcion
            {
                Id = dto.Id,
                Texto = dto.Texto,
                CapituloDestino = new CapituloIntermedio
                {
                    Id = dto.CapituloDestinoId
                }
            };
        }
    }
}