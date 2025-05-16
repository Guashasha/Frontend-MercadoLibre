using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models
{
    public class UsuarioPwd
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es correo válido.")]
        [Display(Name = "Correo electrónico")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener un mínino de {1} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string Rol { get; set; }
    }
}
