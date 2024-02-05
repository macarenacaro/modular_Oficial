using System.ComponentModel.DataAnnotations;

namespace modulAR_M.Caro.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El país es un campo requerido.")]
        public string? Pais { get; set; }

        [Required(ErrorMessage = "La ciudad es un campo requerido.")]
        public string? Ciudad { get; set; }

        public string? Poblacion { get; set; }

        public ICollection<Proyecto> Proyectos { get; set; }
        public ICollection<Empleo> Empleos { get; set; }

    }
}
