using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioCapitulosEF : IRepositorioCapitulos
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioCapitulosEF(EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Capitulo nuevo)
        {
            nuevo.Validar();

            contexto.Capitulos.Add(nuevo);
            contexto.SaveChanges();
        }

        public Capitulo FindById(int id)
        {
            Capitulo? capitulo = contexto.Capitulos
                .Include(c => c.Historia)
                .FirstOrDefault(c => c.Id == id);

            if (capitulo == null)
            {
                throw new CapituloException("No se encontró el capítulo.");
            }

            return capitulo;
        }

        public List<Capitulo> FindAll()
        {
            return contexto.Capitulos
                .Include(c => c.Historia)
                .OrderBy(c => c.Id)
                .ToList();
        }

        public void Update(Capitulo capitulo)
        {
            capitulo.Validar();
            contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            Capitulo capitulo = FindById(id);

            contexto.Capitulos.Remove(capitulo);
            contexto.SaveChanges();
        }
    }
}