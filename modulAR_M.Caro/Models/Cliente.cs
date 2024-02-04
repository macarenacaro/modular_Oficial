using System.ComponentModel.DataAnnotations;

namespace modulAR_M.Caro.Models
{
    public class Cliente
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "El NIF es un campo requerido.")]
        public string? Nif { get; set; }

        [Required(ErrorMessage = "El nombre es un campo requerido.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es un campo requerido.")]
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Poblacion { get; set; }
        public string? CodigoPostal { get; set; }
        public int? UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<Postulacion> Postulacion { get; set; }
    }
}
