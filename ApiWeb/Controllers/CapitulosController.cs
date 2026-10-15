using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;
using LogicaAplicacion.InterfacesDeCasoDeUso.Opciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapitulosController : ControllerBase
    {
        private readonly IEncontrarCapitulosPorHistoriaCU encontrarPorHistoria;
        private readonly IObtenerCapituloPorIdCU obtenerPorId;
        private readonly IEncontrarOpcionesPorCapituloCU encontrarOpciones;

        public CapitulosController(
            IEncontrarCapitulosPorHistoriaCU encontrarPorHistoria,
            IObtenerCapituloPorIdCU obtenerPorId,
            IEncontrarOpcionesPorCapituloCU encontrarOpciones)
        {
            this.encontrarPorHistoria = encontrarPorHistoria;
            this.obtenerPorId = obtenerPorId;
            this.encontrarOpciones = encontrarOpciones;
        }

        [HttpGet("historia/{historiaId}")]
        public IActionResult GetPorHistoria(int historiaId)
        {
            try
            {
                return Ok(encontrarPorHistoria.EncontrarPorHistoria(historiaId));
            }
            catch (HistoriaException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPorId(int id)
        {
            try
            {
                return Ok(obtenerPorId.ObtenerPorId(id));
            }
            catch (CapituloException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{capituloId}/opciones")]
        public IActionResult GetOpciones(int capituloId)
        {
            return Ok(encontrarOpciones.EncontrarPorCapitulo(capituloId));
        }
    }
}