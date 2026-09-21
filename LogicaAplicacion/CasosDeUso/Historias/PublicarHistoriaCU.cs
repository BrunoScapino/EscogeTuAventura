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

            ValidarEstructura(historia);

            historia.Estado = EstadoHistoria.PUBLICADA;

            repositorioHistorias.Update(historia);
        }

        private void ValidarEstructura(Historia historia)
        {
            historia.Validar();

            if (!historia.Capitulos.Contains(historia.CapituloInicial))
                throw new HistoriaException("El capítulo inicial debe formar parte de la historia.");

            foreach (Capitulo capitulo in historia.Capitulos)
            {
                capitulo.Validar();

                if (capitulo is CapituloIntermedio capituloIntermedio)
                {
                    if (capituloIntermedio.Opciones.Count == 0)
                        throw new HistoriaException("Todos los capítulos intermedios deben tener al menos una opción.");

                    foreach (Opcion opcion in capituloIntermedio.Opciones)
                    {
                        opcion.Validar();
                    }
                }
            }
        }
    }
}