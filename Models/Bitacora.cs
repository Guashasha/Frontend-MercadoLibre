using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models
{
    public class Bitacora
    {
        [Display(Name = "Id")]
        public int? BitacoraId { get; set; }

        [Display(Name = "Acción")]
        public string? Accion {  get; set; }

        [Display(Name = "Elemento Id")]
        public string? ElementoId { get; set; }

        [Display(Name = "IP")]
        public string? IP { get; set; }

        [Display(Name = "Usuario")]
        public string? Usuario {  get; set; }

        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }
    }
}
