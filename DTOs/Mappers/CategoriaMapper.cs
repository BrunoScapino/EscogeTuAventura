using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public static class CategoriaMapper
    {
        public static CategoriaDTO ToDTO(Categoria categoria)
        {
            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }

        public static Categoria ToEntity(CategoriaDTO dto)
        {
            return new Categoria
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };
        }
    }
}