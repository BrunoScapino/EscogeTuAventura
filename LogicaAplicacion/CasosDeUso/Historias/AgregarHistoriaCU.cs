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
    public class AgregarHistoriaCU : IAgregarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public AgregarHistoriaCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public void Agregar(HistoriaDTO historia)
        {
            Historia nuevaHistoria = HistoriaMapper.FromDTO(historia);
            repositorioHistorias.Add(nuevaHistoria);
        }
    }
}