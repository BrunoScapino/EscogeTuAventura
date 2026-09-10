using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioHistorias : IRepositorioHistorias
    {
        private static List<Historia> historias = new List<Historia>();

        public void Add(Historia nuevo)
        {
            nuevo.Validar();
            historias.Add(nuevo);
        }

        public Historia FindById(int id)
        {
            foreach (Historia historia in historias)
            {
                if (historia.Id == id)
                {
                    return historia;
                }
            }

            return null;
        }

        public List<Historia> FindAll()
        {
            return historias;
        }

        public void Update(Historia obj)
        {
            // Todavía no implementado
        }

        public void Remove(int id)
        {
            // Todavía no implementado
        }
    }
}