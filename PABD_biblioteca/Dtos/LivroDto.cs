using System.ComponentModel.DataAnnotations;

namespace PABD_biblioteca.Dtos
{
    public class LivroDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Nome deve ter no mínimo 5 caracteres")]
        public string? Nome { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "quantidade deve ter no mínimo 2 caracteres")]
        public string? QuantidadePaginas { get; set; }

      
    }
}
