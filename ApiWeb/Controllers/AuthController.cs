using ApiWeb.TokenHandler;
using DTOs.DTOs;
using EscogeTuAventura.Excepciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private ILoginUsuarioCU loginUsuario;
        private IConfiguration configuration;

        public AuthController(
            ILoginUsuarioCU loginUsuario,
            IConfiguration configuration)
        {
            this.loginUsuario = loginUsuario;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                UsuarioDTO usuario =
                    loginUsuario.Login(login);

                string token = JWTHandler.GenerarToken(
                    usuario,
                    configuration);

                return Ok(new
                {
                    Token = token,
                    Rol = usuario.Rol.ToString()
                });
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(
                    500,
                    new
                    {
                        Mensaje =
                            "Ocurrió un error al intentar iniciar sesión."
                    });
            }
        }
    }
}