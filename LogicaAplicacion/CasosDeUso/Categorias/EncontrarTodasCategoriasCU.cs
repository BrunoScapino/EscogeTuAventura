using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using System.Collections.Generic;

namespace LogicaAplicacion.CasosDeUso.Categorias
{
    public class EncontrarTodasCategoriasCU : IEncontrarTodasCategoriasCU
    {
        private IRepositorioCategorias repositorioCategorias;

        public EncontrarTodasCategoriasCU(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public List<CategoriaDTO> EncontrarTodas()
        {
            List<Categoria> categorias = repositorioCategorias.FindAll();
            List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();

            foreach (Categoria categoria in categorias)
            {
                categoriasDTO.Add(CategoriaMapper.ToDTO(categoria));
            }

            return categoriasDTO;
        }
    }
}