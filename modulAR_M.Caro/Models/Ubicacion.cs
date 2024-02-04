namespace modulAR_M.Caro.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }

        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Poblacion { get; set; }

        public Empleo? Empleo { get; set; }
        public int EmpleoId { get; set; }

        public Proyecto? Proyecto { get; set; }
        public int ProyectoId { get; set; }

        public ICollection<Proyecto> Proyectos { get; set; }
        public ICollection<Empleo> Empleos { get; set; }

    }
}
