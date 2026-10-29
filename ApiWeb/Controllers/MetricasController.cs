using LogicaAplicacion.InterfacesDeCasoDeUso.Metricas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class MetricasController : ControllerBase
    {
        private readonly IObtenerRankingHistoriasCU obtenerRankingHistorias;
        private readonly IObtenerEstadisticasFinalesCU obtenerEstadisticasFinales;

        public MetricasController(
            IObtenerRankingHistoriasCU obtenerRankingHistorias,
            IObtenerEstadisticasFinalesCU obtenerEstadisticasFinales)
        {
            this.obtenerRankingHistorias = obtenerRankingHistorias;
            this.obtenerEstadisticasFinales = obtenerEstadisticasFinales;
        }

        [HttpGet("ranking-historias")]
        public IActionResult ObtenerRankingHistorias()
        {
            try
            {
                var ranking = obtenerRankingHistorias.Obtener();

                return Ok(ranking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("estadisticas-finales/{idHistoria}")]
        public IActionResult ObtenerEstadisticasFinales(int idHistoria)
        {
            var estadisticas = obtenerEstadisticasFinales.Obtener(idHistoria);

            return Ok(estadisticas);
        }
    }
}