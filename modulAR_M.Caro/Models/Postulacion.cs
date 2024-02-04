using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace modulAR_M.Caro.Models
{
    public class Postulacion
    {
        [Display(Name = "Núm. Postulacion")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es un campo requerido.")]
        public DateTime Fecha { get; set; }

        public string Mensaje { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int EmpleoId { get; set; }
        public Empleo? Empleo { get; set; }

        public int ProcesoId { get; set; }
        public Proceso? Proceso { get; set; }

        public ICollection<Estado>? Estados { get; set; }

    }
}
