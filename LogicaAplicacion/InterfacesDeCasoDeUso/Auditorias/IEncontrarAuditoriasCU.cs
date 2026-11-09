using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias
{
    public interface IEncontrarAuditoriasCU
    {
        List<AuditoriaDTO> Encontrar( int? historiaId,int? administradorId);
    }
}