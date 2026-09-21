using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
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
                    return historia;
            }

            return null;
        }

        public List<Historia> FindAll()
        {
            return new List<Historia>(historias);
        }

        public void Update(Historia historiaActualizada)
        {
            Historia historiaExistente = FindById(historiaActualizada.Id);

            if (historiaExistente == null)
                throw new HistoriaException("La historia no existe.");

            historiaActualizada.Validar();

            int posicion = historias.IndexOf(historiaExistente);
            historias[posicion] = historiaActualizada;
        }

        public void Remove(int id)
        {
            Historia historia = FindById(id);

            if (historia == null)
                throw new HistoriaException("La historia no existe.");

            historias.Remove(historia);
        }
    }
}