using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("livro")]
    public class Livro
    {
        [Key]
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

        public Estoque Estoque { get; set; } // Relacionamento 1 para 1 com Estoque

        // Relacionamento N para N com Categorias
        public List<LivroCategoria> Categorias { get; set; } = new List<LivroCategoria>();

        // Relacionamento N para N com Autores
        public List<AutorLivro> Autores { get; set; } = new List<AutorLivro>();
    }
}
