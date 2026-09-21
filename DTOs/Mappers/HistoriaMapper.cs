using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public class HistoriaMapper
    {
        public static Historia FromDTO(HistoriaDTO dto)
        {
            return new Historia
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Sinopsis = dto.Sinopsis,
                Estado = dto.Estado
            };
        }

        public static HistoriaDTO ToDTO(Historia historia)
        {
            return new HistoriaDTO
            {
                Id = historia.Id,
                Titulo = historia.Titulo,
                Sinopsis = historia.Sinopsis,
                Estado = historia.Estado,
                CategoriasIds = historia.Categorias
                    .Select(categoria => categoria.Id)
                    .ToList(),
                TituloCapituloInicial = historia.CapituloInicial?.Titulo ?? string.Empty,
                TextoCapituloInicial = historia.CapituloInicial?.Texto ?? string.Empty
            };
        }
    }
}