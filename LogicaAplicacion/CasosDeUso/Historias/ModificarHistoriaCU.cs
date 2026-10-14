using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class ModificarHistoriaCU : IModificarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioCategorias repositorioCategorias;

        private IRegistrarAuditoriaCU registrarAuditoria;
        public ModificarHistoriaCU(
            IRepositorioHistorias repositorioHistorias,
            IRepositorioCategorias repositorioCategorias,
            IRegistrarAuditoriaCU registrarAuditoria)
        {
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioCategorias = repositorioCategorias;
            this.registrarAuditoria = registrarAuditoria;
        }

        public void Modificar(HistoriaDTO historia, int administradorId)
        {
            Historia existente = repositorioHistorias.FindById(historia.Id);

            EstadoHistoria estadoAnterior = existente.Estado;

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

            AccionAuditoria accion;

            if (estadoAnterior == EstadoHistoria.BORRADOR &&
                existente.Estado == EstadoHistoria.PUBLICADA)
            {
                accion = AccionAuditoria.PUBLICACION;
            }
            else
            {
                accion = AccionAuditoria.EDICION;
            }

            registrarAuditoria.Registrar(
                existente.Id,
                administradorId,
                accion);
        }
    }
}