using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Opciones
{
    public interface IEncontrarOpcionesPorCapituloCU
    {
        List<OpcionDTO> EncontrarPorCapitulo(int capituloId);
    }
}