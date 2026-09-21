using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingresá el nombre de usuario.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresá la contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}