using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas
{
    public interface IEncontrarLecturasPorUsuarioYMesCU
    {
        List<LecturaDTO> Encontrar(
            int usuarioId,
            int mes,
            int anio);
    }
}