using PresentacionLectura.Enum;
using System.Net.Http.Json;

namespace PresentacionLectura.Auxiliar
{
    public class ClienteHttpAuxiliar
    {
        public static HttpResponseMessage EnviarSolicitud(
            string url,
            VerbosHttp verbo,
            object? obj = null,
            string? token = null)
        {
            HttpClient cliente = new HttpClient();
            Task<HttpResponseMessage>? tarea = null;

            if (!string.IsNullOrEmpty(token))
            {
                cliente.DefaultRequestHeaders.Add(
                    "Authorization",
                    "Bearer " + token);
            }

            switch (verbo)
            {
                case VerbosHttp.GET:
                    tarea = cliente.GetAsync(url);
                    break;

                case VerbosHttp.POST:
                    tarea = cliente.PostAsJsonAsync(url, obj);
                    break;

                case VerbosHttp.DELETE:
                    tarea = cliente.DeleteAsync(url);
                    break;

                case VerbosHttp.PUT:
                    tarea = cliente.PutAsJsonAsync(url, obj);
                    break;
            }

            tarea!.Wait();

            return tarea.Result;
        }

        public static string ObtenerBody(HttpResponseMessage respuesta)
        {
            Task<string> tarea = respuesta.Content.ReadAsStringAsync();

            tarea.Wait();

            return tarea.Result;
        }
    }
}