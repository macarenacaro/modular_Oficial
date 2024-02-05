namespace modulAR_M.Caro.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public int ProyectoId { get; set; }

        public Proyecto? Proyecto { get; set; }
    }
}
