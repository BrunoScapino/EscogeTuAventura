using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private static List<Categoria> categorias = new List<Categoria>();

        public void Add(Categoria nuevo)
        {
            nuevo.Validar();
            categorias.Add(nuevo);
        }

        public Categoria FindById(int id)
        {
            foreach (Categoria categoria in categorias)
            {
                if (categoria.Id == id)
                {
                    return categoria;
                }
            }

            return null;
        }

        public List<Categoria> FindAll()
        {
            return categorias;
        }
    }
}