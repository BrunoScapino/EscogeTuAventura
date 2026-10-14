using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioAuditoriasEF : IRepositorioAuditorias
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioAuditoriasEF(
            EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Auditoria nueva)
        {
            nueva.Validar();

            contexto.Auditorias.Add(nueva);
            contexto.SaveChanges();
        }

        public Auditoria FindById(int id)
        {
            Auditoria? auditoria = contexto.Auditorias
                .Include(auditoria => auditoria.Administrador)
                .Include(auditoria => auditoria.Historia)
                .FirstOrDefault(auditoria => auditoria.Id == id);

            if (auditoria == null)
            {
                throw new AuditoriaException(
                    "No se encontró la auditoría.");
            }

            return auditoria;
        }

        public List<Auditoria> FindAll()
        {
            return Buscar(null, null);
        }

        public List<Auditoria> Buscar(
            int? historiaId,
            int? administradorId)
        {
            return contexto.Auditorias
                .Include(auditoria => auditoria.Administrador)
                .Include(auditoria => auditoria.Historia)
                .Where(auditoria =>
                    (!historiaId.HasValue ||
                        auditoria.Historia.Id == historiaId.Value) &&
                    (!administradorId.HasValue ||
                        auditoria.Administrador.Id ==
                        administradorId.Value))
                .OrderByDescending(auditoria => auditoria.Fecha)
                .ToList();
        }

        public void Update(Auditoria auditoria)
        {
            throw new AuditoriaException(
                "Las auditorías no se pueden modificar.");
        }

        public void Remove(int id)
        {
            throw new AuditoriaException(
                "Las auditorías no se pueden eliminar.");
        }
    }
}