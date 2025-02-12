using System.ComponentModel.DataAnnotations;

namespace PABD_biblioteca.Dtos
{
    public class UsuarioDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        [MaxLength(15)]
        public string Cep { get; set; }

        [MaxLength(100)]
        public string Rua { get; set; }

        [MaxLength(100)]
        public string Bairro { get; set; }

        [MaxLength(10)]
        public string Numero { get; set; }
    }
}
