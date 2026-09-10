using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public class LecturaMapper
    {
        public static LecturaDTO ToDTO(Lectura lectura)
        {
            LecturaDTO dto = new LecturaDTO();

            dto.Id = lectura.Id;
            dto.UsuarioId = lectura.Usuario.Id;
            dto.HistoriaId = lectura.Historia.Id;
            dto.FechaInicio = lectura.FechaInicio;
            dto.FechaFinalizacion = lectura.FechaFinalizacion;

            if (lectura.FinalAlcanzado != null)
            {
                dto.CapituloFinalId = lectura.FinalAlcanzado.Id;
            }

            return dto;
        }
    }
}