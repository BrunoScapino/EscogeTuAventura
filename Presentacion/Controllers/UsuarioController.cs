using DTOs.DTOs;
using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Presentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private IAgregarUsuarioCU agregarUsuario;
        private IEncontrarTodosUsuariosCU encontrarTodosUsuarios;

        public UsuarioController(IAgregarUsuarioCU agregarUsuarioCU,IEncontrarTodosUsuariosCU encontrarTodosUsuariosCU)
        {
            agregarUsuario = agregarUsuarioCU;
            encontrarTodosUsuarios = encontrarTodosUsuariosCU;
        }

        public IActionResult Index()
        {
            return View(encontrarTodosUsuarios.EncontrarTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UsuarioDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            try
            {
                agregarUsuario.Agregar(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (UsuarioException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(usuario);
        }
    }
}