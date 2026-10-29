using Microsoft.AspNetCore.Mvc;
using PresentacionLectura.Auxiliar;
using PresentacionLectura.Enum;
using PresentacionLectura.Models;
using System.Text.Json;

namespace PresentacionLectura.Controllers
{
    public class MetricasController : Controller
    {
        private readonly IConfiguration configuracion;

        public MetricasController(IConfiguration configuracion)
        {
            this.configuracion = configuracion;
        }

        public IActionResult RankingHistorias()
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            string url = configuracion["ApiUrl"] + "/api/Metricas/ranking-historias";

            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                url,
                VerbosHttp.GET,
                null,
                token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudo obtener el ranking de historias.";
                return View(new List<RankingHistoriaApiDTO>());
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<RankingHistoriaApiDTO>? ranking =
                JsonSerializer.Deserialize<List<RankingHistoriaApiDTO>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return View(ranking ?? new List<RankingHistoriaApiDTO>());
        }
    }

}