using Microsoft.EntityFrameworkCore;
using WebApp_Autores.Entidades;

namespace WebApp_Autores
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Autor>  Autores { get; set; }  /*Son las tablas en sql server*/
        public DbSet<Libro> Libros { get; set; }

    }
}
