using DTOs.DTOs;
using DTOs.Mappers;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Opciones;

namespace LogicaAplicacion.CasosDeUso.Opciones
{
    public class EncontrarOpcionesPorCapituloCU
        : IEncontrarOpcionesPorCapituloCU
    {
        private readonly IRepositorioOpciones repositorioOpciones;

        public EncontrarOpcionesPorCapituloCU( IRepositorioOpciones repositorioOpciones)
        {
            this.repositorioOpciones = repositorioOpciones;
        }

        public List<OpcionDTO> EncontrarPorCapitulo(int capituloId)
        {
            return repositorioOpciones
                .FindAll()
                .Where(opcion => opcion.CapituloOrigen.Id == capituloId)
                .Select(opcion => OpcionMapper.ToDTO(opcion))
                .ToList();
        }
    }
}