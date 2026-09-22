using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class EliminarHistoriaCU : IEliminarHistoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public EliminarHistoriaCU(
            IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public void Eliminar(int id)
        {
            repositorioHistorias.Remove(id);
        }
    }
}