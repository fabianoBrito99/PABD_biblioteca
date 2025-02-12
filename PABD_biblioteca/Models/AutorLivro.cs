using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("autor_livro")]
    public class AutorLivro
    {
        [Column("fk_id_autor")]
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        [Column("fk_id_livro")]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
