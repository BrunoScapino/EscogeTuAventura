using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public class CategoriaMapper
    {
        public static Categoria FromDTO(CategoriaDTO dto)
        {
            return new Categoria
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };
        }

        public static CategoriaDTO ToDTO(Categoria categoria)
        {
            return new CategoriaDTO()
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }
    }
}