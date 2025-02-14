using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("livro")]
    public class Livro
    {
        [Column("id_livro")]
        public int Id { get; set; }

        [Column("nome_livro")]
        public string Nome { get; set; }

        [Column("quantidade_paginas")]
        public int? QuantidadePaginas { get; set; }

        [Column("descricao")]
        public string? Descricao { get; set; }

        [Column("ano_publicacao")]
        public int? AnoPublicacao { get; set; }

        [Column("quantidade_estoque")]
        public int QuantidadeEstoque { get; set; }
    }
}
