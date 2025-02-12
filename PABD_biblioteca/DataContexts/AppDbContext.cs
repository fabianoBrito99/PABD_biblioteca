using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.Models;

namespace PABD_biblioteca.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<UsuarioEmprestimo> UsuarioEmprestimos { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
  
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<AutorLivro> AutorLivros { get; set; }
        public DbSet<LivroCategoria> LivroCategorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuração da tabela de junção Usuario_Emprestimos
            modelBuilder.Entity<UsuarioEmprestimo>()
                .HasKey(ue => new { ue.UsuarioId, ue.EmprestimoId });

            modelBuilder.Entity<UsuarioEmprestimo>()
                .HasOne(ue => ue.Usuario)
                .WithMany(u => u.UsuarioEmprestimos)
                .HasForeignKey(ue => ue.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioEmprestimo>()
                .HasOne(ue => ue.Emprestimo)
                .WithMany(e => e.UsuarioEmprestimos)
                .HasForeignKey(ue => ue.EmprestimoId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuração da tabela de junção Autor_Livro
            modelBuilder.Entity<AutorLivro>()
                .HasKey(al => new { al.AutorId, al.LivroId });

            modelBuilder.Entity<AutorLivro>()
                .HasOne(al => al.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(al => al.AutorId);

            modelBuilder.Entity<AutorLivro>()
                .HasOne(al => al.Livro)
                .WithMany(l => l.Autores)
                .HasForeignKey(al => al.LivroId);

            // Configuração da tabela de junção Livro_Categoria
            modelBuilder.Entity<LivroCategoria>()
                .HasKey(lc => new { lc.LivroId, lc.CategoriaId });

            modelBuilder.Entity<LivroCategoria>()
                .HasOne(lc => lc.Livro)
                .WithMany(l => l.Categorias)
                .HasForeignKey(lc => lc.LivroId);

            modelBuilder.Entity<LivroCategoria>()
                .HasOne(lc => lc.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(lc => lc.CategoriaId);
        }

    }
}
