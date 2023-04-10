using Microsoft.EntityFrameworkCore;
using RegistroUsuarios.Modelo;

namespace RegistroUsuarios.Persistencia
{
    public class DbContextoClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextoClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("ConexionDatabase"));

        }

        public DbSet<Usuarios> Usuarios { get; set; }

    }
}
