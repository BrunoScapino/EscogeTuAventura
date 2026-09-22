using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class AgregarHistoriaCU : IAgregarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioCategorias repositorioCategorias;

        public AgregarHistoriaCU(
         IRepositorioHistorias repositorioHistorias,
         IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioCategorias = repositorioCategorias;
        }

        public void Agregar(HistoriaDTO historia)
        {
            Historia nuevaHistoria = HistoriaMapper.FromDTO(historia);

            nuevaHistoria.Estado = EstadoHistoria.BORRADOR;

            if (historia.CategoriasIds == null || historia.CategoriasIds.Count == 0)
                throw new HistoriaException("La historia debe tener al menos una categoría.");

            foreach (int categoriaId in historia.CategoriasIds)
            {
                Categoria categoria = repositorioCategorias.FindById(categoriaId);

                if (categoria == null)
                    throw new HistoriaException("Una de las categorías indicadas no existe.");

                nuevaHistoria.Categorias.Add(categoria);
            }

            CapituloIntermedio capituloInicial = new CapituloIntermedio
            {
                Titulo = historia.TituloCapituloInicial,
                Texto = historia.TextoCapituloInicial,
                Historia = nuevaHistoria
            };

            nuevaHistoria.CapituloInicial = capituloInicial;
            nuevaHistoria.Capitulos.Add(capituloInicial);

            repositorioHistorias.Add(nuevaHistoria);
            
        }
    }
}