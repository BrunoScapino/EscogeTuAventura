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

        public IActionResult Index()
        {
            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    BaseUrl,
                    VerbosHttp.GET);

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
    }
}