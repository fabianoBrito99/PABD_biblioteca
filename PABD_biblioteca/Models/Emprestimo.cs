using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("emprestimos")]
    public class Emprestimo
    {
        [Column("id_emprestimo")]
        public int Id { get; set; }

        [Column("data_emprestimo")]
        public DateTime DataEmprestimo { get; set; }

        [Column("data_prevista_devolucao")]
        public DateTime DataPrevistaDevolucao { get; set; }

        [Column("data_devolucao")]
        public DateTime? DataDevolucao { get; set; }

        [Column("fk_id_livro")]
        public int LivroId { get; set; }

        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

        // Relacionamento muitos-para-muitos com Usuario
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
