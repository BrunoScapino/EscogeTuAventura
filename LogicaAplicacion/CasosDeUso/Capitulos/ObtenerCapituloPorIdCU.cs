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
    public class ObtenerCapituloPorIdCU : IObtenerCapituloPorIdCU
    {
        private IRepositorioCapitulos repositorioCapitulos;

        public ObtenerCapituloPorIdCU(IRepositorioCapitulos repositorioCapitulos)
        {
            this.repositorioCapitulos = repositorioCapitulos;
        }

        public CapituloDTO ObtenerPorId(int id)
        {
            Capitulo capitulo = repositorioCapitulos.FindById(id);

            if (capitulo == null)
                throw new CapituloException("El capítulo no existe.");

            return CapituloMapper.ToDTO(capitulo);
        }
    }
}