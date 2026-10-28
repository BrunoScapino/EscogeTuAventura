using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas;

namespace LogicaAplicacion.CasosDeUso.Lecturas
{
    public class FinalizarLecturaCU : IFinalizarLecturaCU
    {
        private IRepositorioLecturas repositorioLecturas;
        private IRepositorioCapitulos repositorioCapitulos;

        public FinalizarLecturaCU(
            IRepositorioLecturas repositorioLecturas,
            IRepositorioCapitulos repositorioCapitulos)
        {
            this.repositorioLecturas = repositorioLecturas;
            this.repositorioCapitulos = repositorioCapitulos;
        }

        public LecturaDTO Finalizar(
            int lecturaId,
            int capituloFinalId)
        {
            Lectura lectura =
                repositorioLecturas.FindById(lecturaId);

            if (lectura.FechaFinalizacion.HasValue)
            {
                throw new LecturaException(
                    "La lectura ya fue finalizada.");
            }

            Capitulo capitulo =
                repositorioCapitulos.FindById(capituloFinalId);

            if (capitulo is not CapituloFinal capituloFinal)
            {
                throw new LecturaException(
                    "El capítulo indicado no es un final.");
            }

            if (capituloFinal.Historia.Id != lectura.Historia.Id)
            {
                throw new LecturaException(
                    "El final no pertenece a la historia leída.");
            }

            lectura.FechaFinalizacion = DateTime.Now;
            lectura.FinalAlcanzado = capituloFinal;

            capituloFinal.VecesAlcanzado++;

            repositorioCapitulos.Update(capituloFinal);
            repositorioLecturas.Update(lectura);

            return LecturaMapper.ToDTO(lectura);
        }
    }
}