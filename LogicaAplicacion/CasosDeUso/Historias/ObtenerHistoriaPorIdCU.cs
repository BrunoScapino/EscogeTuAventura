using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class ObtenerHistoriaPorIdCU : IObtenerHistoriaPorIdCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public ObtenerHistoriaPorIdCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public HistoriaDTO ObtenerPorId(int id)
        {
            Historia historia = repositorioHistorias.FindById(id);

            if (historia == null)
            {
                return null;
            }

            return HistoriaMapper.ToDTO(historia);
        }
    }
}