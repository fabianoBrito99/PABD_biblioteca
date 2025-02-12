using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PABD_biblioteca.Models
{
    [Table("emprestimos")]
    public class Emprestimo
    {
        [Key]
        [Column("id_emprestimo")]
        public int Id { get; set; }

        [Column("data_emprestimo")]
        [Required]
        public DateTime DataEmprestimo { get; set; }

        [Column("data_prevista_devolucao")]
        [Required]
        public DateTime DataPrevistaDevolucao { get; set; }

        [Column("data_devolucao")]
        public DateTime? DataDevolucao { get; set; }

        [Column("fk_id_livro")]
        public int LivroId { get; set; }
      

        [JsonIgnore]
        public Livro? Livro { get; set; }

        
        [JsonIgnore]
        public List<UsuarioEmprestimo>? UsuarioEmprestimos { get; set; }
    }
}
