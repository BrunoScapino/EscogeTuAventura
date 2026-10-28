using EscogeTuAventura.Entidades;

namespace EscogeTuAventura.InterfacesDeRepositorio
{
    public interface IRepositorioLecturas : IRepository<Lectura>
    {
        List<Lectura> BuscarPorUsuarioYMes(
            int usuarioId,
            int mes,
            int anio);
    }




}