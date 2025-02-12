using System.ComponentModel.DataAnnotations.Schema;

namespace PABD_biblioteca.Models
{
    [Table("usuario_emprestimos")]
    public class UsuarioEmprestimo
    {
        [Column("fk_id_usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Column("fk_id_emprestimo")]
        public int EmprestimoId { get; set; }
        public Emprestimo Emprestimo { get; set; }
    }
}
