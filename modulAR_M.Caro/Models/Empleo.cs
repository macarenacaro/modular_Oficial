using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace modulAR_M.Caro.Models
{
    public class Empleo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de la empresa es un campo requerido.")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }


        [Required(ErrorMessage = "El título es un campo requerido.")]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es un campo requerido.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "La imagen es un campo requerido.")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "El escaparate es un campo requerido.")]
        public bool? Escaparate { get; set; }

        [Required(ErrorMessage = "El ID de la categoría es un campo requerido.")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [Required(ErrorMessage = "La ubicación es un campo requerido.")]
        public int UbicacionId { get; set; }
        public Ubicacion? Ubicacion { get; set; }
        public ICollection<Postulacion> Postulaciones { get; set; }
    }
}
