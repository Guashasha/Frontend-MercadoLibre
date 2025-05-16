using System.ComponentModel.DataAnnotations;
namespace frontendnet.Models;

public class Rol
{
    public required string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [Display(Name = "Rol")]
    public required string Nombre { get; set; }
}
