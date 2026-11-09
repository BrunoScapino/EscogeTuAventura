using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Metricas;

namespace LogicaAplicacion.CasosDeUso.Metricas
{
    public class ObtenerRankingHistoriasCU : IObtenerRankingHistoriasCU
    {
        private readonly IRepositorioLecturas repositorioLecturas;

        public ObtenerRankingHistoriasCU(IRepositorioLecturas repositorioLecturas)
        {
            this.repositorioLecturas = repositorioLecturas;
        }

        public List<RankingHistoriaDTO> Obtener()
        {
            List<Lectura> lecturas = repositorioLecturas.FindAll();

            List<RankingHistoriaDTO> ranking = lecturas
                .Where(lectura => lectura.FechaFinalizacion.HasValue)
                .GroupBy(lectura => new
                {
                    lectura.Historia.Id,
                    lectura.Historia.Titulo
                })
                .Select(grupo => new RankingHistoriaDTO
                {
                    TituloHistoria = grupo.Key.Titulo,
                    CantidadLecturasCompletadas = grupo.Count()
                })
                .OrderByDescending(historia => historia.CantidadLecturasCompletadas)
                .ToList();

            return ranking;
        }
    }
}