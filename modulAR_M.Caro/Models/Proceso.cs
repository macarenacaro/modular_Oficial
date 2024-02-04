using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace modulAR_M.Caro.Models
{
    public class Proceso
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es un campo requerido.")]
        public string? Descripcion { get; set; }
        public ICollection<Postulacion>? Postulaciones { get; set; }

    }
}
