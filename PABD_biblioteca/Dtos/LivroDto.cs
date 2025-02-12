using System.ComponentModel.DataAnnotations;

namespace PABD_biblioteca.Dtos
{
    public class LivroDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "O nome deve ter no mínimo 5 caracteres")]
        public string Nome { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade de páginas deve ser maior que 0")]
        public int? QuantidadePaginas { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "A descrição deve ter no mínimo 5 caracteres")]
        public string Descricao { get; set; }

        [Required]
        [Range(1800, 2025, ErrorMessage = "Ano de publicação inválido")]
        public int? AnoPublicacao { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade em estoque deve ser maior ou igual a zero")]
        public int QuantidadeEstoque { get; set; }

        public List<string> AutoresNomes { get; set; } = new List<string>();
        public List<string> CategoriasNomes { get; set; } = new List<string>();
    }
}
