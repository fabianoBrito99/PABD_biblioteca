using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [Column("id_categoria")]
        public int Id { get; set; }

        [Column("nome_categoria")]
        public string Nome { get; set; }
    }
}
