using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("livro_categoria")]
    public class LivroCategoria
    {
        [Column("fk_id_livros")]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }

        [Column("fk_id_categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
