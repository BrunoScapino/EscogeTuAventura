using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;

namespace LogicaAplicacion.CasosDeUso.Capitulos
{
    public class EncontrarCapitulosPorHistoriaCU : IEncontrarCapitulosPorHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioCapitulos repositorioCapitulos;

        public EncontrarCapitulosPorHistoriaCU(
            IRepositorioHistorias repositorioHistorias,
            IRepositorioCapitulos repositorioCapitulos)
        {
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioCapitulos = repositorioCapitulos;
        }

        public List<CapituloDTO> EncontrarPorHistoria(int historiaId)
        {
            Historia historia = repositorioHistorias.FindById(historiaId);

            if (historia == null)
                throw new HistoriaException("La historia no existe.");

            return repositorioCapitulos
                .FindAll()
                .Where(capitulo => capitulo.Historia.Id == historiaId)
                .Select(capitulo => CapituloMapper.ToDTO(capitulo))
                .ToList();
        }
    }
}