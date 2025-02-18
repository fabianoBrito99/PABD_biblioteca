using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PABD_biblioteca.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [Key]
        [Column("id_categoria")]
        public int Id { get; set; }

        [Column("nome_categoria")]
        public string? Nome { get; set; }

        [JsonIgnore]
        public List<LivroCategoria> Livros { get; set; } = new List<LivroCategoria>();
    }
}
