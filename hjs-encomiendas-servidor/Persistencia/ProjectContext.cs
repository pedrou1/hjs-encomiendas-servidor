using hjs_encomiendas_servidor.Modelo;
using Microsoft.EntityFrameworkCore;

namespace hjs_encomiendas_servidor.Persistencia
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions options)
        : base(options)
        {
        }
        
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<CategoriaUsuario> CategoriaUsuario { get; set; }

        public DbSet<UnidadTransporte> UnidadTransporte { get; set; }

        public DbSet<Estado> Estado { get; set; }

        public DbSet<Pedido> Pedido { get; set; }

        public DbSet<EstadoPedido> EstadoPedido { get; set; }

        public DbSet<TipoPedido> TipoPedido { get; set; }
    }
}
