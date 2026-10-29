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
    public class ObtenerEstadisticasFinalesCU : IObtenerEstadisticasFinalesCU
    {
        private readonly IRepositorioHistorias repositorioHistorias;

        public ObtenerEstadisticasFinalesCU(
            IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public List<EstadisticaFinalDTO> Obtener(int idHistoria)
        {
            Historia historia = repositorioHistorias.FindById(idHistoria);

            List<CapituloFinal> finales = historia.Capitulos
                .OfType<CapituloFinal>()
                .ToList();

            List<EstadisticaFinalDTO> estadisticas = new List<EstadisticaFinalDTO>();

            if (finales.Count == 0)
            {
                return estadisticas;
            }

            int maximoAlcanzado = finales.Max(
                final => final.VecesAlcanzado);

            foreach (CapituloFinal final in finales)
            {
                EstadisticaFinalDTO estadistica = new EstadisticaFinalDTO
                {
                    IdHistoria = historia.Id,
                    TituloHistoria = historia.Titulo,
                    IdFinal = final.Id,
                    TituloFinal = final.Titulo,
                    VecesAlcanzado = final.VecesAlcanzado,
                    EsFinalMasAlcanzado =
                        final.VecesAlcanzado == maximoAlcanzado
                };

                estadisticas.Add(estadistica);
            }

            return estadisticas;
        }
    }
}