using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas;

namespace LogicaAplicacion.CasosDeUso.Lecturas
{
    public class IniciarLecturaCU : IIniciarLecturaCU
    {
        private IRepositorioLecturas repositorioLecturas;
        private IRepositorioUsuarios repositorioUsuarios;
        private IRepositorioHistorias repositorioHistorias;

        public IniciarLecturaCU(
            IRepositorioLecturas repositorioLecturas,
            IRepositorioUsuarios repositorioUsuarios,
            IRepositorioHistorias repositorioHistorias)
        {
            this.repositorioLecturas = repositorioLecturas;
            this.repositorioUsuarios = repositorioUsuarios;
            this.repositorioHistorias = repositorioHistorias;
        }

        public LecturaDTO Iniciar(int usuarioId, int historiaId)
        {
            Usuario usuario = repositorioUsuarios.FindById(usuarioId);

            if (usuario.Rol != Rol.Lector)
            {
                throw new LecturaException("Solo los lectores pueden iniciar una lectura.");
            }

            Historia historia = repositorioHistorias.FindById(historiaId);

            if (historia.Estado != EstadoHistoria.PUBLICADA)
            {
                throw new LecturaException("Solo se pueden leer historias publicadas.");
            }

            Lectura nuevaLectura = new Lectura
            {
                Usuario = usuario,
                Historia = historia,
                FechaInicio = DateTime.Now
            };

            repositorioLecturas.Add(nuevaLectura);

            return LecturaMapper.ToDTO(nuevaLectura);
        }
    }
}