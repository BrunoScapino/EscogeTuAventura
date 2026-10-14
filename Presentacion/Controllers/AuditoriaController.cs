using DTOs.DTOs;
using EscogeTuAventura.Entidades;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AuditoriaController : Controller
    {
        private IEncontrarAuditoriasCU encontrarAuditorias;
        private IEncontrarTodasHistoriasCU encontrarHistorias;
        private IEncontrarTodosUsuariosCU encontrarUsuarios;

        public AuditoriaController(
            IEncontrarAuditoriasCU encontrarAuditorias,
            IEncontrarTodasHistoriasCU encontrarHistorias,
            IEncontrarTodosUsuariosCU encontrarUsuarios)
        {
            this.encontrarAuditorias = encontrarAuditorias;
            this.encontrarHistorias = encontrarHistorias;
            this.encontrarUsuarios = encontrarUsuarios;
        }

        public IActionResult Index(
            int? historiaId,
            int? administradorId)
        {
            CargarFiltros(historiaId, administradorId);

            List<AuditoriaDTO> auditorias =
                encontrarAuditorias.Encontrar(
                    historiaId,
                    administradorId);

            return View(auditorias);
        }

        private void CargarFiltros(
            int? historiaId,
            int? administradorId)
        {
            var historias = encontrarHistorias.EncontrarTodas();

            var administradores = encontrarUsuarios
                .EncontrarTodos()
                .Where(usuario =>
                    usuario.Rol == Rol.Administrador)
                .ToList();

            ViewBag.Historias = new SelectList(
                historias,
                "Id",
                "Titulo",
                historiaId);

            ViewBag.Administradores = new SelectList(
                administradores,
                "Id",
                "NombreUsuario",
                administradorId);
        }
    }
}