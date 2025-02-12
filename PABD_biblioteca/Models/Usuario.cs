using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PABD_biblioteca.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("nome_usuario")]
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Column("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column("senha")]
        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }

        [Column("telefone")]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [Column("cep")]
        [MaxLength(15)]
        public string Cep { get; set; }

        [Column("rua")]
        [MaxLength(100)]
        public string Rua { get; set; }

        [Column("bairro")]
        [MaxLength(100)]
        public string Bairro { get; set; }

        [Column("numero")]
        [MaxLength(10)]
        public string Numero { get; set; }

     
        [JsonIgnore]
        public List<UsuarioEmprestimo>? UsuarioEmprestimos { get; set; } = new List<UsuarioEmprestimo>();
    }
}
