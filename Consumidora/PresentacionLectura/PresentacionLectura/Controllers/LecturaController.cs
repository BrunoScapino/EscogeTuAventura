using Microsoft.AspNetCore.Mvc;
using PresentacionLectura.Auxiliar;
using PresentacionLectura.Enum;
using PresentacionLectura.Models;
using System.Text.Json;

namespace PresentacionLectura.Controllers
{
    public class LecturaController : Controller
    {
        /*private const string CapitulosUrl =
            "https://localhost:7265/api/Capitulos";

        private const string LecturasUrl =
            "https://localhost:7265/api/Lecturas";*/

        private const string CapitulosUrl =
            "http://escojetuaventura.somee.com/api/Capitulos";

        private const string LecturasUrl =
            "http://escojetuaventura.somee.com/api/Lecturas";

        public IActionResult Iniciar(
            int historiaId,
            int capituloInicialId)
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{LecturasUrl}/iniciar/{historiaId}",
                    VerbosHttp.POST,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                TempData["Error"] =
                    "No fue posible iniciar la lectura.";

                return RedirectToAction("Index", "Historia");
            }

            string body =
                ClienteHttpAuxiliar.ObtenerBody(respuesta);

            LecturaApiDTO? lectura =
                JsonSerializer.Deserialize<LecturaApiDTO>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (lectura == null)
            {
                TempData["Error"] =
                    "La API no devolvió una lectura válida.";

                return RedirectToAction("Index", "Historia");
            }

            HttpContext.Session.SetString(
                "lecturaActualId",
                lectura.Id.ToString());

            return RedirectToAction(
                nameof(Capitulo),
                new { id = capituloInicialId });
        }

        public IActionResult Capitulo(int id)
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{CapitulosUrl}/{id}",
                    VerbosHttp.GET,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "No fue posible obtener el capítulo.";

                return View(new CapituloApiDTO());
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            CapituloApiDTO? capitulo =
                JsonSerializer.Deserialize<CapituloApiDTO>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (capitulo == null)
            {
                ViewBag.Error =
                    "No fue posible interpretar el capítulo recibido.";

                return View(new CapituloApiDTO());
            }

            if (capitulo.EsFinal)
            {
                FinalizarLectura(id, token);
            }
            else
            {
                ViewBag.Opciones =
                    ObtenerOpciones(id, token);
            }

            return View(capitulo);
        }

        public IActionResult Historial(int? mes, int? anio)
        {
            string? token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            int mesSeleccionado = mes ?? DateTime.Now.Month;
            int anioSeleccionado = anio ?? DateTime.Now.Year;

            ViewBag.Mes = mesSeleccionado;
            ViewBag.Anio = anioSeleccionado;

            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{LecturasUrl}/historial?mes={mesSeleccionado}&anio={anioSeleccionado}",
                    VerbosHttp.GET,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "No fue posible obtener el historial de lecturas.";

                return View(new List<LecturaApiDTO>());
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<LecturaApiDTO>? lecturas =
                JsonSerializer.Deserialize<List<LecturaApiDTO>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return View(lecturas ?? new List<LecturaApiDTO>());
        }

        private List<OpcionApiDTO> ObtenerOpciones(
            int capituloId,
            string token)
        {
            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{CapitulosUrl}/{capituloId}/opciones",
                    VerbosHttp.GET,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                return new List<OpcionApiDTO>();
            }

            string body = ClienteHttpAuxiliar.ObtenerBody(respuesta);

            List<OpcionApiDTO>? opciones =
                JsonSerializer.Deserialize<List<OpcionApiDTO>>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return opciones ?? new List<OpcionApiDTO>();
        }

        private void FinalizarLectura(
            int capituloFinalId,
            string token)
        {
            string? lecturaIdTexto =
                HttpContext.Session.GetString("lecturaActualId");

            if (!int.TryParse(
                lecturaIdTexto,
                out int lecturaId))
            {
                ViewBag.ErrorFinalizacion =
                    "No se encontró la lectura que se debía finalizar.";

                return;
            }

            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{LecturasUrl}/{lecturaId}/finalizar/{capituloFinalId}",
                    VerbosHttp.PUT,
                    null,
                    token);

            if (!respuesta.IsSuccessStatusCode)
            {
                ViewBag.ErrorFinalizacion =
                    "No fue posible registrar el final alcanzado.";

                return;
            }

            HttpContext.Session.Remove("lecturaActualId");

            ViewBag.MensajeFinalizacion =
                "La lectura fue finalizada correctamente.";
        }
    }
}