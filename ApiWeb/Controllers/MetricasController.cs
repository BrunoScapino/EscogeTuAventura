using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicaAplicacion.InterfacesDeCasoDeUso.Metricas;
using Microsoft.AspNetCore.Authorization;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class MetricasController : ControllerBase
    {
        private readonly IObtenerRankingHistoriasCU obtenerRankingHistorias;

        public MetricasController(
            IObtenerRankingHistoriasCU obtenerRankingHistorias)
        {
            this.obtenerRankingHistorias = obtenerRankingHistorias;
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
    }
}