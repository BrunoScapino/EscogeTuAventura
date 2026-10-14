using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;

namespace LogicaAplicacion.CasosDeUso.Capitulos
{
    public class AgregarCapituloCU : IAgregarCapituloCU
    {
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioCapitulos repositorioCapitulos;
        private IRepositorioOpciones repositorioOpciones;

        private IRegistrarAuditoriaCU registrarAuditoria;

        public AgregarCapituloCU(
            IRepositorioHistorias repositorioHistorias,
            IRepositorioCapitulos repositorioCapitulos,
            IRepositorioOpciones repositorioOpciones,
            IRegistrarAuditoriaCU registrarAuditoria)
        {
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioCapitulos = repositorioCapitulos;
            this.repositorioOpciones = repositorioOpciones;
            this.registrarAuditoria = registrarAuditoria;
        }

        public void Agregar(CapituloDTO capitulo, int administradorId)
        {
            Historia historia = repositorioHistorias.FindById(capitulo.HistoriaId);

            if (historia == null)
                throw new HistoriaException("La historia no existe.");

            Capitulo capituloAnterior = repositorioCapitulos.FindById(capitulo.CapituloAnteriorId);

            if (capituloAnterior == null)
                throw new CapituloException("El capítulo anterior no existe.");

            if (capituloAnterior.Historia.Id != historia.Id)
                throw new CapituloException("El capítulo anterior no pertenece a la historia indicada.");

            if (capituloAnterior is not CapituloIntermedio capituloIntermedio)
                throw new CapituloException("Solo se pueden agregar capítulos después de un capítulo intermedio.");

            Capitulo nuevoCapitulo;

            if (capitulo.EsFinal)
            {
                nuevoCapitulo = new CapituloFinal
                {
                    Titulo = capitulo.Titulo,
                    Texto = capitulo.Texto,
                    Historia = historia,
                    VecesAlcanzado = 0
                };
            }
            else
            {
                nuevoCapitulo = new CapituloIntermedio
                {
                    Titulo = capitulo.Titulo,
                    Texto = capitulo.Texto,
                    Historia = historia
                };
            }

            repositorioCapitulos.Add(nuevoCapitulo);

            Opcion nuevaOpcion = new Opcion
            {
                Texto = capitulo.TextoOpcion,
                CapituloOrigen = capituloIntermedio,
                CapituloDestino = nuevoCapitulo
            };

            repositorioOpciones.Add(nuevaOpcion);

            registrarAuditoria.Registrar(
                historia.Id,
                administradorId,
                AccionAuditoria.CREACION);
        }
    }
}