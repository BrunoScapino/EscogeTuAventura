using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class ModificarHistoriaCU : IModificarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioCategorias repositorioCategorias;

        public ModificarHistoriaCU(
            IRepositorioHistorias repositorioHistorias,
            IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioCategorias = repositorioCategorias;
        }

        public void Modificar(HistoriaDTO historia)
        {
            Historia existente = repositorioHistorias.FindById(historia.Id);

            if (historia.Estado != EstadoHistoria.BORRADOR &&
                historia.Estado != EstadoHistoria.PUBLICADA)
            {
                throw new HistoriaException("El estado indicado no es válido.");
            }

            if (historia.CategoriasIds == null ||
                historia.CategoriasIds.Count == 0)
            {
                throw new HistoriaException(
                    "La historia debe tener al menos una categoría.");
            }

            var categorias = new List<Categoria>();

            foreach (int categoriaId in historia.CategoriasIds.Distinct())
            {
                categorias.Add(
                    repositorioCategorias.FindById(categoriaId));
            }

            bool cambiaCapituloInicial =
                existente.CapituloInicial.Titulo != historia.TituloCapituloInicial ||
                existente.CapituloInicial.Texto != historia.TextoCapituloInicial;

            if (existente.Capitulos.Count > 1 && cambiaCapituloInicial)
            {
                throw new HistoriaException(
                    "No se puede modificar el capítulo inicial " +
                    "porque la historia ya tiene otros capítulos.");
            }

            existente.Titulo = historia.Titulo;
            existente.Sinopsis = historia.Sinopsis;

            existente.Categorias.Clear();

            foreach (Categoria categoria in categorias)
            {
                existente.Categorias.Add(categoria);
            }

            if (existente.Capitulos.Count == 1)
            {
                existente.CapituloInicial.Titulo =
                    historia.TituloCapituloInicial;

                existente.CapituloInicial.Texto =
                    historia.TextoCapituloInicial;
            }

            existente.Validar();
            existente.CapituloInicial.Validar();

            if (historia.Estado == EstadoHistoria.PUBLICADA)
            {
                existente.ValidarPublicacion();
            }

            existente.Estado = historia.Estado;

            repositorioHistorias.Update(existente);
        }
    }
}