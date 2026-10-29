using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Metricas;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.Metricas
{
    public class ObtenerRankingUsuariosCU : IObtenerRankingUsuariosCU
    {
        private readonly IRepositorioLecturas repositorioLecturas;

        public ObtenerRankingUsuariosCU(
            IRepositorioLecturas repositorioLecturas)
        {
            this.repositorioLecturas = repositorioLecturas;
        }

        public List<RankingUsuarioDTO> Obtener()
        {
            List<Lectura> lecturas = repositorioLecturas.FindAll();

            List<RankingUsuarioDTO> ranking = lecturas
                .Where(lectura => lectura.FechaFinalizacion.HasValue)
                .GroupBy(lectura => lectura.Usuario.Id)
                .Select(grupo => new RankingUsuarioDTO
                {
                    NombreUsuario = grupo.First().Usuario.NombreUsuario,
                    CantidadHistoriasCompletadas = grupo
                        .Select(lectura => lectura.Historia.Id)
                        .Distinct()
                        .Count()
                })
                .OrderByDescending(usuario => usuario.CantidadHistoriasCompletadas)
                .ToList();

            return ranking;
        }
    }
}