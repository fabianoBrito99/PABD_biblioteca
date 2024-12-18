using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("autor")]
    public class Autor
    {
        [Column("id_autor")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }
    }
}
