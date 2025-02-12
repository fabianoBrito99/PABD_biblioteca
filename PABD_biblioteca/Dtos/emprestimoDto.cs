using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PABD_biblioteca.Dtos
{
    public class EmprestimoDto
    {
        [Required]
        public int LivroId { get; set; } 

        [Required]
        public List<int> UsuariosIds { get; set; }

        [Required]
        public DateTime DataEmprestimo { get; set; }

        [Required]
        public DateTime DataPrevistaDevolucao { get; set; }

        public DateTime? DataDevolucao { get; set; }
    }
}
