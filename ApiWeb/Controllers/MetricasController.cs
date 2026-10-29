using LogicaAplicacion.InterfacesDeCasoDeUso.Metricas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class MetricasController : ControllerBase
    {
        private readonly IObtenerRankingHistoriasCU obtenerRankingHistorias;
        private readonly IObtenerEstadisticasFinalesCU obtenerEstadisticasFinales;
        private readonly IEncontrarTodasHistoriasCU encontrarTodasHistorias;

        public MetricasController(
            IObtenerRankingHistoriasCU obtenerRankingHistorias,
            IObtenerEstadisticasFinalesCU obtenerEstadisticasFinales,
            IEncontrarTodasHistoriasCU encontrarTodasHistorias)
        {
            this.obtenerRankingHistorias = obtenerRankingHistorias;
            this.obtenerEstadisticasFinales = obtenerEstadisticasFinales;
            this.encontrarTodasHistorias = encontrarTodasHistorias;
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

        [HttpGet("historias")]
        public IActionResult ObtenerHistorias()
        {
            var historias = encontrarTodasHistorias.EncontrarTodas();

            return Ok(historias);
        }
    }
}