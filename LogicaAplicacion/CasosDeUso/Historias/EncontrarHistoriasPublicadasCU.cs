using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class EncontrarHistoriasPublicadasCU : IEncontrarHistoriasPublicadasCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public EncontrarHistoriasPublicadasCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public List<HistoriaDTO> EncontrarPublicadas()
        {
            return repositorioHistorias
                .FindAll()
                .Where(historia => historia.Estado == EstadoHistoria.PUBLICADA)
                .Select(historia => HistoriaMapper.ToDTO(historia))
                .ToList();
        }
    }
}
