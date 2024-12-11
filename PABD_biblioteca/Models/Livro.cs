using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("Livro")]
    public class Livros
    {
        [Column("id_livro")]
        public int Id { get; set; }

        [Column("nome_livro")]
        public string? Nome { get; set; }

        [Column("qtd_paginas")]
        public string? QuantidadePaginas { get; set; }

    }
}

