using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioLecturas : IRepositorioLecturas
    {
        private static List<Lectura> lecturas = new List<Lectura>();

        public void Add(Lectura nuevo)
        {
            nuevo.Validar();
            lecturas.Add(nuevo);
        }

        public Lectura FindById(int id)
        {
            foreach (Lectura lectura in lecturas)
            {
                if (lectura.Id == id)
                {
                    return lectura;
                }
            }

            return null;
        }

        public List<Lectura> FindAll()
        {
            return lecturas;
        }

        public void Update(Lectura obj)
        {
            // Todavía no implementado
        }

        public void Remove(int id)
        {
            // Todavía no implementado
        }
    }
}