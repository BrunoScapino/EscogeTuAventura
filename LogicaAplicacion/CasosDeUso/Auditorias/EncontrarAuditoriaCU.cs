using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;

namespace LogicaAplicacion.CasosDeUso.Auditorias
{
    public class EncontrarAuditoriasCU : IEncontrarAuditoriasCU
    {
        private IRepositorioAuditorias repositorioAuditorias;

        public EncontrarAuditoriasCU(
            IRepositorioAuditorias repositorioAuditorias)
        {
            this.repositorioAuditorias = repositorioAuditorias;
        }

        public List<AuditoriaDTO> Encontrar(
            int? historiaId,
            int? administradorId)
        {
            return repositorioAuditorias
                .Buscar(historiaId, administradorId)
                .Select(auditoria => AuditoriaMapper.ToDTO(auditoria))
                .ToList();
        }
    }
}