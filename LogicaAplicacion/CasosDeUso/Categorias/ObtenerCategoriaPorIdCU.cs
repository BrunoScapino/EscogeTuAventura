using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;

namespace LogicaAplicacion.CasosDeUso.Categorias
{
    public class ObtenerCategoriaPorIdCU : IObtenerCategoriaPorIdCU
    {
        private IRepositorioCategorias repositorioCategorias;

        public ObtenerCategoriaPorIdCU(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public CategoriaDTO ObtenerPorId(int id)
        {
            Categoria categoria = repositorioCategorias.FindById(id);

            if (categoria == null)
            {
                return null;
            }

            return CategoriaMapper.ToDTO(categoria);
        }
    }
}