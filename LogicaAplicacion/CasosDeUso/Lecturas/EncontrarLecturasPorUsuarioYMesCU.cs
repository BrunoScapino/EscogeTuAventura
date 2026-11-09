using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas;

namespace LogicaAplicacion.CasosDeUso.Lecturas
{
    public class EncontrarLecturasPorUsuarioYMesCU
        : IEncontrarLecturasPorUsuarioYMesCU
    {
        private IRepositorioLecturas repositorioLecturas;

        public EncontrarLecturasPorUsuarioYMesCU( IRepositorioLecturas repositorioLecturas)
        {
            this.repositorioLecturas = repositorioLecturas;
        }

        public List<LecturaDTO> Encontrar(int usuarioId,int mes,int anio)
        {
            if (mes < 1 || mes > 12)
            {
                throw new LecturaException("El mes debe estar entre 1 y 12.");
            }

            if (anio < 1)
            {
                throw new LecturaException("El año indicado no es válido.");
            }

            return repositorioLecturas
                .BuscarPorUsuarioYMes(usuarioId, mes, anio)
                .Select(lectura => LecturaMapper.ToDTO(lectura))
                .ToList();
        }
    }
}