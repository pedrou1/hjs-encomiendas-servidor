using hjs_encomiendas_servidor.Modelo;
using Microsoft.EntityFrameworkCore;

namespace hjs_encomiendas_servidor.Persistencia
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<CategoriaUsuario> CategoriaUsuario { get; set; }

    }
}
