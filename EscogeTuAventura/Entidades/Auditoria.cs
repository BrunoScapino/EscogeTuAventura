using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDelDominio;

namespace EscogeTuAventura.Entidades
{
    public class Auditoria : IValidable
    {
        public int Id { get; set; }

        public AccionAuditoria Accion { get; set; }

        public DateTime Fecha { get; set; }

        public Usuario Administrador { get; set; } = null!;

        public Historia Historia { get; set; } = null!;

        public void Validar()
        {
            if (!Enum.IsDefined(typeof(AccionAuditoria), Accion))
            {
                throw new AuditoriaException(
                    "La acción de auditoría no es válida.");
            }

            if (Fecha == default)
            {
                throw new AuditoriaException(
                    "La fecha de la auditoría es requerida.");
            }

            if (Administrador == null)
            {
                throw new AuditoriaException(
                    "La auditoría debe tener un administrador.");
            }

            if (Administrador.Rol != Rol.Administrador)
            {
                throw new AuditoriaException(
                    "La auditoría debe ser realizada por un administrador.");
            }

            if (Historia == null)
            {
                throw new AuditoriaException(
                    "La auditoría debe tener una historia.");
            }
        }
    }
}