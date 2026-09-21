using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IEncontrarHistoriasPublicadasPorCategoriaCU
    {
        List<HistoriaDTO> EncontrarPublicadasPorCategoria(int categoriaId);
    }
}
