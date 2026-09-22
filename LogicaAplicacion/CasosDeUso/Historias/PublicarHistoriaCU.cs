using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class PublicarHistoriaCU : IPublicarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public PublicarHistoriaCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public void Publicar(int id)
        {
            Historia historia = repositorioHistorias.FindById(id);

            if (historia == null)
                throw new HistoriaException("La historia no existe.");

            historia.ValidarPublicacion();

            historia.Estado = EstadoHistoria.PUBLICADA;

            repositorioHistorias.Update(historia);
        }

        
    }
}