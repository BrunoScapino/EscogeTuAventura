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

        public IActionResult EstadisticasFinales(int idHistoria)
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            string rol = HttpContext.Session.GetString("rol") ?? string.Empty;

            if (rol != "Administrador")
            {
                return RedirectToAction("Index", "Historia");
            }

            string url = configuracion["ApiUrl"]
                + "/api/Metricas/estadisticas-finales/"
                + idHistoria;

            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                url,
                VerbosHttp.GET,
                null,
                token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudieron obtener las estadísticas de los finales.";
                return View(new List<EstadisticaFinalModel>());
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<EstadisticaFinalModel>? estadisticas =
                JsonSerializer.Deserialize<List<EstadisticaFinalModel>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return View(estadisticas ?? new List<EstadisticaFinalModel>());
        }

        public IActionResult SeleccionarHistoria()
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            string rol = HttpContext.Session.GetString("rol") ?? string.Empty;

            if (rol != "Administrador")
            {
                return RedirectToAction("Index", "Historia");
            }

            string url = configuracion["ApiUrl"] + "/api/Historias";

            HttpResponseMessage respuesta = ClienteHttpAuxiliar.EnviarSolicitud(
                url,
                VerbosHttp.GET,
                null,
                token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudieron obtener las historias.";
                return View(new List<SeleccionHistoriaEstadisticasModel>());
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<SeleccionHistoriaEstadisticasModel>? historias =
                JsonSerializer.Deserialize<List<SeleccionHistoriaEstadisticasModel>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return View(historias ?? new List<SeleccionHistoriaEstadisticasModel>());
        }
    }
}