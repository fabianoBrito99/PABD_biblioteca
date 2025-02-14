using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.Models;

namespace PABD_biblioteca.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Autor> Autores { get; set; }


    }
}
