using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioAuditorias : IRepositorioAuditorias
    {
        private static List<Auditoria> auditorias = new List<Auditoria>();

        public void Add(Auditoria nuevo)
        {
            nuevo.Validar();
            auditorias.Add(nuevo);
        }

        public Auditoria FindById(int id)
        {
            foreach (Auditoria auditoria in auditorias)
            {
                if (auditoria.Id == id)
                {
                    return auditoria;
                }
            }

            return null;
        }

        public List<Auditoria> FindAll()
        {
            return auditorias;
        }

        public void Update(Auditoria obj)
        {
            // Todavía no implementado
        }

        public void Remove(int id)
        {
            // Todavía no implementado
        }
    }
}