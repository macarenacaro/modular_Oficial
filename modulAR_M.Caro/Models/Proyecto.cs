using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace modulAR_M.Caro.Models
{
    public class Proyecto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del cliente es un campo requerido.")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }


        [Required(ErrorMessage = "El título es un campo requerido.")]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es un campo requerido.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es un campo requerido.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }


        [Required(ErrorMessage = "El escaparate es un campo requerido.")]
        public bool Escaparate { get; set; }


        [Required(ErrorMessage = "La imagen es un campo requerido.")]
        public string Imagen { get; set; }


        [Required(ErrorMessage = "La venta es un campo requerido.")]
        public bool Venta { get; set; }

        [Required(ErrorMessage = "La realidad aumentada es un campo requerido.")]
        public bool RAumentada { get; set; }

        public string ImagenRA { get; set; }


        [Required(ErrorMessage = "El ID de la categoría es un campo requerido.")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        [Required(ErrorMessage = "La ubicación es un campo requerido.")]
        public int UbicacionId { get; set; }
        public Ubicacion? Ubicacion { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Detalle>? Detalles { get; set; }

    }
}
