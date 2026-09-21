using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
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
                    return capitulo;
            }

            return null;
        }

        public List<Capitulo> FindAll()
        {
            return new List<Capitulo>(capitulos);
        }

        public void Update(Capitulo capituloActualizado)
        {
            Capitulo capituloExistente = FindById(capituloActualizado.Id);

            if (capituloExistente == null)
                throw new CapituloException("El capítulo no existe.");

            capituloActualizado.Validar();

            int posicion = capitulos.IndexOf(capituloExistente);
            capitulos[posicion] = capituloActualizado;
        }

        public void Remove(int id)
        {
            Capitulo capitulo = FindById(id);

            if (capitulo == null)
                throw new CapituloException("El capítulo no existe.");

            capitulos.Remove(capitulo);
        }
    }
}