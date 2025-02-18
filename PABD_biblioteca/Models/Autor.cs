using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PABD_biblioteca.Models
{
    [Table("autor")]
    public class Autor
    {
        [Key]
        [Column("id_autor")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [JsonIgnore]
        public List<AutorLivro> Livros { get; set; } = new List<AutorLivro>();
    }
}
