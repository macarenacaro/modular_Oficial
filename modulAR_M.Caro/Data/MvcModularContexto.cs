using Microsoft.EntityFrameworkCore;
using modulAR_M.Caro.Models;

namespace modulAR_M.Caro.Data
{
    public class MvcModularContexto : DbContext
    {
        public MvcModularContexto(DbContextOptions<MvcModularContexto> options)
        : base(options)
        {
        }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Comentario>? Comentarios { get; set; }
        public DbSet<Detalle>? Detalles { get; set; }
        public DbSet<Empleo>? Empleos { get; set; }
        public DbSet<Estado>? Estados { get; set; }
        public DbSet<Pedido>? Pedidos { get; set; }
        public DbSet<Postulacion>? Postulaciones { get; set; }
        public DbSet<Proceso>? Procesos { get; set; }
        public DbSet<Proyecto>? Proyectos { get; set; }
        public DbSet<Ubicacion>? Ubicaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Deshabilitar la eliminación en cascada en todas las relaciones
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in
            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
