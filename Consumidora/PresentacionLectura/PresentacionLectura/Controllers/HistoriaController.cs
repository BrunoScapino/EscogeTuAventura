using Microsoft.AspNetCore.Mvc;
using PresentacionLectura.Auxiliar;
using PresentacionLectura.Enum;
using PresentacionLectura.Models;
using System.Text.Json;

namespace PresentacionLectura.Controllers
{
    public class HistoriaController : Controller
    {
        private const string BaseUrl =
            "https://localhost:7265/api/Historias";

        private const string CategoriasUrl =
            "https://localhost:7265/api/Categorias";

        public IActionResult Index(int? categoriaId)
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            CargarCategorias(token);

            string url = BaseUrl;

            if (categoriaId.HasValue)
            {
                url = $"{BaseUrl}/categoria/{categoriaId.Value}";
            }

            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    url,
                    VerbosHttp.GET,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "No fue posible obtener las historias publicadas.";

                return View(new List<HistoriaApiDTO>());
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<HistoriaApiDTO>? historias =
                JsonSerializer.Deserialize<List<HistoriaApiDTO>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return View(historias ?? new List<HistoriaApiDTO>());
        }

        private void CargarCategorias(string token)
        {
            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    CategoriasUrl,
                    VerbosHttp.GET,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Categorias = new List<CategoriaApiDTO>();
                return;
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<CategoriaApiDTO>? categorias =
                JsonSerializer.Deserialize<List<CategoriaApiDTO>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            ViewBag.Categorias =
                categorias ?? new List<CategoriaApiDTO>();
        }
    }
}