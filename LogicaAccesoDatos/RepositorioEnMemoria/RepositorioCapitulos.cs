using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioCapitulos : IRepositorioCapitulos
    {
        private static List<Capitulo> capitulos = new List<Capitulo>();

        public void Add(Capitulo nuevo)
        {
            nuevo.Validar();
            capitulos.Add(nuevo);
        }

        public Capitulo FindById(int id)
        {
            foreach (Capitulo capitulo in capitulos)
            {
                if (capitulo.Id == id)
                {
                    return capitulo;
                }
            }

            return null;
        }

        public List<Capitulo> FindAll()
        {
            return capitulos;
        }

        public void Update(Capitulo obj)
        {
            // Todavía no implementado
        }

        public void Remove(int id)
        {
            // Todavía no implementado
        }
    }
}