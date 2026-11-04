using Microsoft.AspNetCore.Mvc;

namespace PresentacionLectura.Controllers
{
    public class TemaController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cambiar(string tema, string? returnUrl)
        {
            if (tema != "claro" && tema != "oscuro")
            {
                tema = "claro";
            }

            HttpContext.Session.SetString("tema", tema);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Historia");
        }
    }
}