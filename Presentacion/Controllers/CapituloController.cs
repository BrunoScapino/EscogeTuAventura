using DTOs.DTOs;
using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;


namespace Presentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CapituloController : Controller
    {
        private IAgregarCapituloCU agregarCapitulo;
        private IEncontrarCapitulosPorHistoriaCU encontrarCapitulos;

        public CapituloController(IAgregarCapituloCU agregarCapitulo,IEncontrarCapitulosPorHistoriaCU encontrarCapitulos)
        {
            this.agregarCapitulo = agregarCapitulo;
            this.encontrarCapitulos = encontrarCapitulos;
        }

        [HttpGet]
        public IActionResult Create(int historiaId)
        {
            try
            {
                CargarCapitulosIntermedios(historiaId);

                return View(new CapituloDTO
                {
                    HistoriaId = historiaId
                });
            }
            catch (HistoriaException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Titulo,Texto,HistoriaId,CapituloAnteriorId,EsFinal,TextoOpcion")]
            CapituloDTO capitulo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    agregarCapitulo.Agregar(capitulo, ObtenerAdministradorId());

                    TempData["Mensaje"] = "El capítulo y su opción se agregaron correctamente.";

                    return RedirectToAction("Index", "Historia");
                }
                catch (HistoriaException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (CapituloException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (OpcionException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (AuditoriaException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            try
            {
                CargarCapitulosIntermedios(capitulo.HistoriaId);
            }
            catch (HistoriaException)
            {
                return NotFound();
            }

            return View(capitulo);
        }

        private void CargarCapitulosIntermedios(int historiaId)
        {
            var capitulos = encontrarCapitulos
                .EncontrarPorHistoria(historiaId)
                .Where(capitulo => !capitulo.EsFinal)
                .ToList();

            ViewBag.CapitulosAnteriores = new SelectList(capitulos,"Id","Titulo");
        }

        private int ObtenerAdministradorId()
        {
            string? administradorId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(administradorId, out int id))
            {
                throw new UsuarioException("No se pudo identificar al administrador logueado.");
            }

            return id;
        }
    }
}