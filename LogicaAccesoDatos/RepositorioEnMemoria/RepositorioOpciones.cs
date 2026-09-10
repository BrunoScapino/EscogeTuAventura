using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioOpciones : IRepositorioOpciones
    {
        private static List<Opcion> opciones = new List<Opcion>();

        public void Add(Opcion nuevo)
        {
            nuevo.Validar();
            opciones.Add(nuevo);
        }

        public Opcion FindById(int id)
        {
            foreach (Opcion opcion in opciones)
            {
                if (opcion.Id == id)
                {
                    return opcion;
                }
            }

            return null;
        }

        public List<Opcion> FindAll()
        {
            return opciones;
        }

        public void Update(Opcion obj)
        {
            // Todavía no implementado
        }

        public void Remove(int id)
        {
            // Todavía no implementado
        }
    }
}