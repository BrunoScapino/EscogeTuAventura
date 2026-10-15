using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IEncontrarTodasCategoriasCU encontrarTodasCategorias;

        public CategoriasController(
            IEncontrarTodasCategoriasCU encontrarTodasCategorias)
        {
            this.encontrarTodasCategorias = encontrarTodasCategorias;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(encontrarTodasCategorias.EncontrarTodas());
        }
    }
}