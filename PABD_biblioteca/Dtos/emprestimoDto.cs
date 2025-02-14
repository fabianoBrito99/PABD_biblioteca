using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PABD_biblioteca.Dtos
{
    internal class emprestimoDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Nome deve ter no mínimo 5 caracteres")]
        public string? DataEmprestimo { get; set; }

   
    }
}
