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
    public class AgregarCategoriaCU : IAgregarCategoriaCU
    {
        private IRepositorioCategorias repositorioCategorias;

        public AgregarCategoriaCU(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public void Agregar(CategoriaDTO categoria)
        {
            Categoria nuevaCategoria = CategoriaMapper.FromDTO(categoria);
            repositorioCategorias.Add(nuevaCategoria);
        }
    }
}