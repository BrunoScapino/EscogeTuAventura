using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioLecturasEF : IRepositorioLecturas
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioLecturasEF(
            EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Lectura nueva)
        {
            nueva.Validar();

            contexto.Lecturas.Add(nueva);
            contexto.SaveChanges();
        }

        public Lectura FindById(int id)
        {
            Lectura? lectura = contexto.Lecturas
                .Include(lectura => lectura.Usuario)
                .Include(lectura => lectura.Historia)
                .Include(lectura => lectura.FinalAlcanzado)
                .FirstOrDefault(lectura => lectura.Id == id);

            if (lectura == null)
            {
                throw new LecturaException(
                    "No se encontró la lectura.");
            }

            return lectura;
        }

        public List<Lectura> FindAll()
        {
            return contexto.Lecturas
                .Include(lectura => lectura.Usuario)
                .Include(lectura => lectura.Historia)
                .Include(lectura => lectura.FinalAlcanzado)
                .ToList();
        }
        public List<Lectura> BuscarPorUsuarioYMes(
            int usuarioId,
            int mes,
            int anio)
        {
            return contexto.Lecturas
                .Include(lectura => lectura.Usuario)
                .Include(lectura => lectura.Historia)
                .Include(lectura => lectura.FinalAlcanzado)
                .Where(lectura =>
                    lectura.Usuario.Id == usuarioId &&
                    lectura.FechaInicio.Month == mes &&
                    lectura.FechaInicio.Year == anio)
                .OrderByDescending(lectura => lectura.FechaInicio)
                .ToList();
        }

        public void Update(Lectura lectura)
        {
            lectura.Validar();

            contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            Lectura lectura = FindById(id);

            contexto.Lecturas.Remove(lectura);
            contexto.SaveChanges();
        }
    }
}