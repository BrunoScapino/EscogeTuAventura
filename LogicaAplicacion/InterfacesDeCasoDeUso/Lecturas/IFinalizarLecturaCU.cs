using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas
{
    public interface IFinalizarLecturaCU
    {
        LecturaDTO Finalizar(
            int lecturaId,
            int capituloFinalId,
            int usuarioId);
    }
}