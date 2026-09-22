using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioCategoriasEF : IRepositorioCategorias
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioCategoriasEF(EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Categoria nueva)
        {
            nueva.Validar();

            contexto.Categorias.Add(nueva);
            contexto.SaveChanges();
        }

        public List<Categoria> FindAll()
        {
            return contexto.Categorias
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        public Categoria FindById(int id)
        {
            Categoria? categoria = contexto.Categorias
                .FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                throw new CategoriaException(
                    "No se encontró la categoría.");
            }

            return categoria;
        }

        public void Update(Categoria nueva)
        {
            nueva.Validar();

            Categoria existente = FindById(nueva.Id);
            existente.Nombre = nueva.Nombre;

            contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            Categoria categoria = FindById(id);

            bool estaEnUso = contexto.Historias
                .Any(h => h.Categorias.Any(c => c.Id == id));

            if (estaEnUso)
            {
                throw new CategoriaException(
                    "No se puede eliminar una categoría asociada a historias.");
            }

            contexto.Categorias.Remove(categoria);
            contexto.SaveChanges();
        }
    }
}