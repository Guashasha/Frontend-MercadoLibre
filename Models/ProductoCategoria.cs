using System.ComponentModel.DataAnnotations;
namespace frontendnet.Models;

public class ProductoCategoria
{
    [Display(Name = "Categoría")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public int? CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public Producto? Producto { get; set; }
}
