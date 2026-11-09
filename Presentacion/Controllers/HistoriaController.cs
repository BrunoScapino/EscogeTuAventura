using DTOs.DTOs;
using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class HistoriaController : Controller
    {
        private IAgregarHistoriaCU agregarHistoria;
        private IEncontrarTodasHistoriasCU encontrarHistorias;
        private IEncontrarTodasCategoriasCU encontrarCategorias;
        private IObtenerHistoriaPorIdCU obtenerHistoria;
        private IModificarHistoriaCU modificarHistoria;
        private IEliminarHistoriaCU eliminarHistoria;

        public HistoriaController(
            IAgregarHistoriaCU agregarHistoria,
            IEncontrarTodasHistoriasCU encontrarHistorias,
            IEncontrarTodasCategoriasCU encontrarCategorias,
            IObtenerHistoriaPorIdCU obtenerHistoria,
            IModificarHistoriaCU modificarHistoria,
            IEliminarHistoriaCU eliminarHistoria)
        {
            this.agregarHistoria = agregarHistoria;
            this.encontrarHistorias = encontrarHistorias;
            this.encontrarCategorias = encontrarCategorias;
            this.obtenerHistoria = obtenerHistoria;
            this.modificarHistoria = modificarHistoria;
            this.eliminarHistoria = eliminarHistoria;
        }

        public IActionResult Index()
        {
            return View(encontrarHistorias.EncontrarTodas());
        }

        [HttpGet]
        public IActionResult Create()
        {
            CargarCategorias();
            return View(new HistoriaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Titulo,Sinopsis,CategoriasIds,TituloCapituloInicial,TextoCapituloInicial")]
            HistoriaDTO historia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    agregarHistoria.Agregar(historia, ObtenerAdministradorId());

                    TempData["Mensaje"] = "La historia se creó correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (HistoriaException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (CapituloException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (CategoriaException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (AuditoriaException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            CargarCategorias();
            return View(historia);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                HistoriaDTO historia = obtenerHistoria.ObtenerPorId(id);

                if (historia == null)
                {
                    return NotFound();
                }

                CargarCategorias();
                return View(historia);
            }
            catch (HistoriaException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, HistoriaDTO historia)
        {
            if (id != historia.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    modificarHistoria.Modificar(historia, ObtenerAdministradorId());

                    TempData["Mensaje"] = "La historia se modificó correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (HistoriaException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (CategoriaException ex)
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

            CargarCategorias();
            return View(historia);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                HistoriaDTO historia = obtenerHistoria.ObtenerPorId(id);

                if (historia == null)
                {
                    return NotFound();
                }

                return View(historia);
            }
            catch (HistoriaException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmado(int id)
        {
            try
            {
                eliminarHistoria.Eliminar(id);
                TempData["Mensaje"] = "La historia se eliminó correctamente.";
            }
            catch (HistoriaException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
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
        private void CargarCategorias()
        {
            var categorias = encontrarCategorias.EncontrarTodas();

            ViewBag.Categorias = new MultiSelectList(categorias,"Id","Nombre");

            ViewBag.HayCategorias = categorias.Count > 0;
        }
    }
}