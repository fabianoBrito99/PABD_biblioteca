using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("nome_usuario")]
        public string Nome { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("telefone")]
        public string? Telefone { get; set; }

        [Column("cep")]
        public string? Cep { get; set; }

        [Column("rua")]
        public string? Rua { get; set; }

        [Column("bairro")]
        public string? Bairro { get; set; }

        [Column("numero")]
        public string? Numero { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
