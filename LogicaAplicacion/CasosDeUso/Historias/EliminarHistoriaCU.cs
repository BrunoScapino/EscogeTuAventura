using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class EliminarHistoriaCU : IEliminarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioLecturas repositorioLecturas;

        public EliminarHistoriaCU(
            IRepositorioHistorias repositorioHistorias,
            IRepositorioLecturas repositorioLecturas)
        {
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioLecturas = repositorioLecturas;
        }

        public void Eliminar(int id)
        {
            Historia historia = repositorioHistorias.FindById(id);

            if (historia == null)
                throw new HistoriaException("La historia no existe.");

            bool tieneLecturas = repositorioLecturas
                .FindAll()
                .Any(lectura => lectura.Historia.Id == id);

            if (tieneLecturas)
                throw new HistoriaException("No se puede eliminar la historia porque tiene lecturas asociadas.");

            repositorioHistorias.Remove(id);
        }
    }
}
