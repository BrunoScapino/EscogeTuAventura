using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Lector")]
    public class CategoriasController : ControllerBase
    {
        private readonly IEncontrarTodasCategoriasCU encontrarTodasCategorias;

        public CategoriasController(IEncontrarTodasCategoriasCU encontrarTodasCategorias)
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