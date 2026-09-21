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

        public ModificarHistoriaCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public void Modificar(HistoriaDTO historia)
        {
            Historia historiaExistente = repositorioHistorias.FindById(historia.Id);

            if (historiaExistente == null)
                throw new HistoriaException("La historia no existe.");

            historiaExistente.Titulo = historia.Titulo;
            historiaExistente.Sinopsis = historia.Sinopsis;
            historiaExistente.Validar();

            repositorioHistorias.Update(historiaExistente);
        }
    }
}
