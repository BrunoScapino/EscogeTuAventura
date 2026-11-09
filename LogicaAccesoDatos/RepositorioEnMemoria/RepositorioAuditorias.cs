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
        public List<Auditoria> Buscar(int? historiaId,int? administradorId)
        {
            return auditorias
                .Where(auditoria => (!historiaId.HasValue || auditoria.Historia.Id == historiaId.Value) && (!administradorId.HasValue ||auditoria.Administrador.Id == administradorId.Value))
                .OrderByDescending(auditoria => auditoria.Fecha)
                .ToList();
        }
    }
}