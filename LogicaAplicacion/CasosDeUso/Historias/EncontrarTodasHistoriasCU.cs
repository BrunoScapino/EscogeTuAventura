using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using System.Collections.Generic;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class EncontrarTodasHistoriasCU : IEncontrarTodasHistoriasCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public EncontrarTodasHistoriasCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public List<HistoriaDTO> EncontrarTodas()
        {
            List<Historia> historias = repositorioHistorias.FindAll();
            List<HistoriaDTO> historiasDTO = new List<HistoriaDTO>();

            foreach (Historia historia in historias)
            {
                historiasDTO.Add(HistoriaMapper.ToDTO(historia));
            }

            return historiasDTO;
        }
    }
}