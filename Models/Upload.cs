using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models
{
    public class Upload
    {
        [Display(Name = "Id")]
        public int? ArchivoId { get; set; }

        public string? Nombre { get; set; }

        [Display(Name = "Portada")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Upload)]
        public required IFormFile Portada { get; set; }
    }
}
