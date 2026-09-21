using System.Security.Claims;
using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    public class CuentaController : Controller
    {
        private ILoginAdministradorCU loginAdministrador;

        public CuentaController(ILoginAdministradorCU loginAdministrador)
        {
            this.loginAdministrador = loginAdministrador;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            try
            {
                var usuario = loginAdministrador.Login(
                    modelo.NombreUsuario,
                    modelo.Password);

                var datos = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        usuario.Id.ToString()),

                    new Claim(
                        ClaimTypes.Name,
                        usuario.NombreUsuario),

                    new Claim(
                        ClaimTypes.Role,
                        usuario.Rol.ToString())
                };

                var identidad = new ClaimsIdentity(
                    datos,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identidad);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return RedirectToAction("Index", "Usuario");
            }
            catch (UsuarioException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(modelo);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return StatusCode(
                403,
                "No tenés permisos para acceder a esta página.");
        }
    }
}