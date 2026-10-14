using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioOpcionesEF : IRepositorioOpciones
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioOpcionesEF(EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Opcion nueva)
        {
            nueva.Validar();

            contexto.Opciones.Add(nueva);
            contexto.SaveChanges();
        }

        public Opcion FindById(int id)
        {
            Opcion? opcion = contexto.Opciones
                .Include(o => o.CapituloOrigen)
                .Include(o => o.CapituloDestino)
                .FirstOrDefault(o => o.Id == id);

            if (opcion == null)
            {
                throw new OpcionException("No se encontró la opción.");
            }

            return opcion;
        }

        public List<Opcion> FindAll()
        {
            return contexto.Opciones
                .Include(o => o.CapituloOrigen)
                .Include(o => o.CapituloDestino)
                .ToList();
        }

        public void Update(Opcion opcion)
        {
            opcion.Validar();
            contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            Opcion opcion = FindById(id);

            contexto.Opciones.Remove(opcion);
            contexto.SaveChanges();
        }
    }
}