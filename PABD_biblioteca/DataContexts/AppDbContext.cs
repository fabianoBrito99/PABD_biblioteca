using PABD_biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace PABD_biblioteca.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Livros> Livros { get; set; }
    }
}
