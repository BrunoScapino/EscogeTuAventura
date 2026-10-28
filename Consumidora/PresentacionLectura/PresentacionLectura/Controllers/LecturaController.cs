using Microsoft.AspNetCore.Mvc;
using PresentacionLectura.Auxiliar;
using PresentacionLectura.Enum;
using PresentacionLectura.Models;
using System.Text.Json;

namespace PresentacionLectura.Controllers
{
    public class LecturaController : Controller
    {
        private const string CapitulosUrl =
            "https://localhost:7265/api/Capitulos";

        public IActionResult Iniciar(int capituloInicialId)
        {
            return RedirectToAction(
                nameof(Capitulo),
                new { id = capituloInicialId });
        }

        public IActionResult Capitulo(int id)
        {
            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{CapitulosUrl}/{id}",
                    VerbosHttp.GET);

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

            if (!capitulo.EsFinal)
            {
                ViewBag.Opciones = ObtenerOpciones(id);
            }

            return View(capitulo);
        }

        private List<OpcionApiDTO> ObtenerOpciones(int capituloId)
        {
            HttpResponseMessage respuesta =
                ClienteHttpAuxiliar.EnviarSolicitud(
                    $"{CapitulosUrl}/{capituloId}/opciones",
                    VerbosHttp.GET);

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
    }
}