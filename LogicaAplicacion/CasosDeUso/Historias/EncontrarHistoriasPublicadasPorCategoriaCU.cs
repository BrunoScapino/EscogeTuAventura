using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace LogicaAplicacion.CasosDeUso.Historias
{
    public class EncontrarHistoriasPublicadasPorCategoriaCU : IEncontrarHistoriasPublicadasPorCategoriaCU
    {
        private IRepositorioHistorias repositorioHistorias;

        public EncontrarHistoriasPublicadasPorCategoriaCU(IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioHistorias = repositorioHistorias;
        }

        public List<HistoriaDTO> EncontrarPublicadasPorCategoria(int categoriaId)
        {
            return repositorioHistorias
                .FindAll()
                .Where(historia => historia.Estado == EstadoHistoria.PUBLICADA)
                .Where(historia => historia.Categorias.Any(categoria => categoria.Id == categoriaId))
                .Select(historia => HistoriaMapper.ToDTO(historia))
                .ToList();
        }
    }
}
