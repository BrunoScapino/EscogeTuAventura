using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriasController : ControllerBase
    {
        private readonly IEncontrarHistoriasPublicadasCU encontrarHistoriasPublicadas;
        private readonly IEncontrarHistoriasPublicadasPorCategoriaCU encontrarPorCategoria;

        public HistoriasController(
            IEncontrarHistoriasPublicadasCU encontrarHistoriasPublicadas,
            IEncontrarHistoriasPublicadasPorCategoriaCU encontrarPorCategoria)
        {
            this.encontrarHistoriasPublicadas = encontrarHistoriasPublicadas;
            this.encontrarPorCategoria = encontrarPorCategoria;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(encontrarHistoriasPublicadas.EncontrarPublicadas());
        }

        [HttpGet("categoria/{categoriaId}")]
        public IActionResult GetPorCategoria(int categoriaId)
        {
            return Ok(
                encontrarPorCategoria
                    .EncontrarPublicadasPorCategoria(categoriaId));
        }
    }
}