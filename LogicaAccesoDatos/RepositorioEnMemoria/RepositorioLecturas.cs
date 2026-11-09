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

        public List<Lectura> BuscarPorUsuarioYMes(
            int usuarioId,
            int mes,
            int anio)
        {
            return lecturas
                .Where(lectura => lectura.Usuario.Id == usuarioId && lectura.FechaInicio.Month == mes && lectura.FechaInicio.Year == anio)
                .OrderByDescending(lectura => lectura.FechaInicio)
                .ToList();
        }
    }
}