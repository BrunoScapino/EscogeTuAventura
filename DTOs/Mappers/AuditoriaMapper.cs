using DTOs.DTOs;
using EscogeTuAventura.Entidades;

namespace DTOs.Mappers
{
    public static class AuditoriaMapper
    {
        public static AuditoriaDTO ToDTO(Auditoria auditoria)
        {
            return new AuditoriaDTO
            {
                Id = auditoria.Id,
                Accion = auditoria.Accion.ToString(),
                Fecha = auditoria.Fecha,
                AdministradorId = auditoria.Administrador.Id,
                AdministradorNombreUsuario =
                    auditoria.Administrador.NombreUsuario,
                HistoriaId = auditoria.Historia.Id,
                HistoriaTitulo = auditoria.Historia.Titulo
            };
        }

        public static Auditoria ToEntity(AuditoriaDTO dto)
        {
            return new Auditoria
            {
                Id = dto.Id,
                Accion = Enum.Parse<AccionAuditoria>(dto.Accion),
                Fecha = dto.Fecha,
                Administrador = new Usuario
                {
                    Id = dto.AdministradorId
                },
                Historia = new Historia
                {
                    Id = dto.HistoriaId
                }
            };
        }
    }
}