using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioHistoriasEF : IRepositorioHistorias
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioHistoriasEF(EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Historia nueva)
        {
            nueva.Validar();
            nueva.CapituloInicial.Validar();

            CapituloIntermedio inicial = nueva.CapituloInicial;

            if (nueva.Estado != EstadoHistoria.BORRADOR ||
                nueva.Capitulos.Count != 1 ||
                nueva.Capitulos[0] != inicial ||
                inicial.Historia != nueva ||
                inicial.Opciones.Count != 0)
            {
                throw new HistoriaException(
                    "La historia debe crearse en borrador, con un único " +
                    "capítulo inicial sin opciones.");
            }

            using var transaccion = contexto.Database.BeginTransaction();

            try
            {
                // Primero guardamos la historia sin enlazar el capítulo.
                nueva.CapituloInicial = null!;
                nueva.Capitulos.Clear();

                contexto.Historias.Add(nueva);
                contexto.SaveChanges();

                // Después guardamos el capítulo con la historia ya creada.
                nueva.Capitulos.Add(inicial);
                contexto.Capitulos.Add(inicial);
                contexto.SaveChanges();

                // Finalmente indicamos cuál es el capítulo inicial.
                nueva.CapituloInicial = inicial;
                contexto.SaveChanges();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                contexto.ChangeTracker.Clear();
                throw;
            }
            finally
            {
                nueva.CapituloInicial = inicial;

                if (!nueva.Capitulos.Contains(inicial))
                {
                    nueva.Capitulos.Add(inicial);
                }
            }
        }

        public List<Historia> FindAll()
        {
            return contexto.Historias
                .Include(h => h.Categorias)
                .Include(h => h.CapituloInicial)
                .OrderBy(h => h.Titulo)
                .ToList();
        }

        public Historia FindById(int id)
        {
            Historia? historia = contexto.Historias
                .Include(h => h.Categorias)
                .Include(h => h.CapituloInicial)
                .Include(h => h.Capitulos)
                .FirstOrDefault(h => h.Id == id);

            if (historia == null)
            {
                throw new HistoriaException("No se encontró la historia.");
            }

            contexto.Opciones
                .Where(o => o.CapituloOrigen.Historia.Id == id)
                .Include(o => o.CapituloDestino)
                .Load();

            return historia;
        }

        public void Update(Historia historia)
        {
            historia.Validar();

            if (historia.Estado == EstadoHistoria.PUBLICADA)
            {
                historia.ValidarPublicacion();
            }

            contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            using var transaccion = contexto.Database.BeginTransaction(
                System.Data.IsolationLevel.Serializable);

            try
            {
                Historia historia = FindById(id);

                bool tieneLecturas = contexto.Lecturas
                    .Any(lectura => lectura.Historia.Id == id);

                if (tieneLecturas)
                {
                    throw new HistoriaException(
                        "No se puede eliminar la historia porque tiene lecturas asociadas.");
                }

                // Eliminamos las opciones de los capítulos de esta historia.
                var opciones = contexto.Opciones
                    .Where(opcion => opcion.CapituloOrigen.Historia.Id == id)
                    .ToList();

                contexto.Opciones.RemoveRange(opciones);

                // Quitamos la referencia al capítulo inicial antes de borrarlo.
                historia.CapituloInicial = null!;

                // Quitamos las asociaciones, conservando las categorías.
                historia.Categorias.Clear();

                contexto.SaveChanges();

                // Eliminamos los capítulos.
                contexto.Capitulos.RemoveRange(historia.Capitulos.ToList());
                contexto.SaveChanges();

                // Finalmente eliminamos la historia.
                contexto.Historias.Remove(historia);
                contexto.SaveChanges();

                transaccion.Commit();
            }
            catch (DbUpdateException ex)
            {
                transaccion.Rollback();
                contexto.ChangeTracker.Clear();

                throw new HistoriaException(
                    "No se pudo eliminar la historia porque existen datos relacionados " +
                    "que impiden completar la operación.",
                    ex);
            }
            catch
            {
                transaccion.Rollback();
                contexto.ChangeTracker.Clear();
                throw;
            }
        }
    }
}