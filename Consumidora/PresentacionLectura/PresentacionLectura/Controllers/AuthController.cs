using Microsoft.AspNetCore.Mvc;
using PresentacionLectura.Auxiliar;
using PresentacionLectura.Enum;
using PresentacionLectura.Models;
using System.Text.Json;

namespace PresentacionLectura.Controllers
{
    public class AuthController : Controller
    {
        private const string LoginUrl =
            "https://localhost:7265/api/Auth/login";

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    LoginUrl,
                    VerbosHttp.POST,
                    model);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "Usuario o contraseña incorrectos.";

                return View(model);
            }

            string body =
                ClienteHttpAuxiliar.ObtenerBody(respuesta);

            TokenModel? tokenModel =
                JsonSerializer.Deserialize<TokenModel>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (tokenModel == null ||
                string.IsNullOrWhiteSpace(tokenModel.Token))
            {
                ViewBag.Error =
                    "No se recibió un token válido desde la API.";

                return View(model);
            }

            HttpContext.Session.SetString(
                "token",
                tokenModel.Token);

            HttpContext.Session.SetString(
                "rol",
                tokenModel.Rol);

            return RedirectToAction(
                "Index",
                "Historia");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("rol");

            return RedirectToAction(nameof(Login));
        }
    }
}