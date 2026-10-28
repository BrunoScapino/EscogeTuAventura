using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas
{
    public interface IIniciarLecturaCU
    {
        LecturaDTO Iniciar(int usuarioId, int historiaId);
    }
}