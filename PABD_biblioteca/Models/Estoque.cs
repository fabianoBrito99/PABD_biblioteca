using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("estoque")]
    public class Estoque
    {
        [Key]
        [Column("id_estoque")]
        public int Id { get; set; }

        [Column("quantidade_estoque")]
        public int Quantidade { get; set; }

        [Column("fk_id_livro")]
        public int LivroId { get; set; }

        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }
    }
}
