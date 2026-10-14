using EscogeTuAventura.Entidades;

namespace EscogeTuAventura.InterfacesDeRepositorio
{
    public interface IRepositorioAuditorias : IRepository<Auditoria>
    {
        List<Auditoria> Buscar(
            int? historiaId,
            int? administradorId);
    }
}