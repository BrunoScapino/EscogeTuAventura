using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
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
                    return opcion;
            }

            return null;
        }

        public List<Opcion> FindAll()
        {
            return new List<Opcion>(opciones);
        }

        public void Update(Opcion opcionActualizada)
        {
            Opcion opcionExistente = FindById(opcionActualizada.Id);

            if (opcionExistente == null)
                throw new OpcionException("La opción no existe.");

            opcionActualizada.Validar();

            int posicion = opciones.IndexOf(opcionExistente);
            opciones[posicion] = opcionActualizada;
        }

        public void Remove(int id)
        {
            Opcion opcion = FindById(id);

            if (opcion == null)
                throw new OpcionException("La opción no existe.");

            opciones.Remove(opcion);
        }
    }
}