using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Lector")]
    public class LecturasController : ControllerBase
    {
        private readonly IIniciarLecturaCU iniciarLectura;
        private readonly IFinalizarLecturaCU finalizarLectura;
        private readonly IEncontrarLecturasPorUsuarioYMesCU encontrarLecturas;

        public LecturasController(
            IIniciarLecturaCU iniciarLectura,
            IFinalizarLecturaCU finalizarLectura,
            IEncontrarLecturasPorUsuarioYMesCU encontrarLecturas)
        {
            this.iniciarLectura = iniciarLectura;
            this.finalizarLectura = finalizarLectura;
            this.encontrarLecturas = encontrarLecturas;
        }

        [HttpPost("iniciar/{historiaId}")]
        public IActionResult Iniciar(int historiaId)
        {
            int? usuarioId = ObtenerUsuarioId();

            if (!usuarioId.HasValue)
            {
                return Unauthorized();
            }

            try
            {
                var lectura = iniciarLectura.Iniciar(usuarioId.Value,historiaId);

                return StatusCode(201, lectura);
            }
            catch (LecturaException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{lecturaId}/finalizar/{capituloFinalId}")]
        public IActionResult Finalizar(int lecturaId,int capituloFinalId)
        {
            int? usuarioId = ObtenerUsuarioId();

            if (!usuarioId.HasValue)
            {
                return Unauthorized();
            }

            try
            {
                var lectura = finalizarLectura.Finalizar(lecturaId,capituloFinalId,usuarioId.Value);

                return Ok(lectura);
            }
            catch (LecturaException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("historial")]
        public IActionResult Historial(int mes, int anio)
        {
            int? usuarioId = ObtenerUsuarioId();

            if (!usuarioId.HasValue)
            {
                return Unauthorized();
            }

            try
            {
                var lecturas = encontrarLecturas.Encontrar(usuarioId.Value,mes,anio);

                return Ok(lecturas);
            }
            catch (LecturaException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        private int? ObtenerUsuarioId()
        {
            string? idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(idUsuario, out int usuarioId))
            {
                return usuarioId;
            }

            return null;
        }
    }
}