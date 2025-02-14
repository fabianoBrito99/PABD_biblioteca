using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;

namespace PABD_biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmprestimoController(AppDbContext context)
        {
            _context = context;
        }

        // Listar todos os empréstimos de um usuário
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetEmprestimosByUsuario(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Emprestimos)
                .ThenInclude(e => e.Livro)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
                return NotFound($"Usuário #{usuarioId} não encontrado.");

            return Ok(usuario.Emprestimos);
        }

        // Buscar um empréstimo específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emprestimo = await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound($"Empréstimo #{id} não encontrado.");

            return Ok(emprestimo);
        }

        // Criar um novo empréstimo para um usuário
        [HttpPost("{usuarioId}/adicionar-emprestimo")]
        public async Task<IActionResult> AdicionarEmprestimoParaUsuario(int usuarioId, [FromBody] Emprestimo emprestimo)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
                return NotFound($"Usuário #{usuarioId} não encontrado.");

            var livro = await _context.Livros.FindAsync(emprestimo.LivroId);
            if (livro == null || livro.QuantidadeEstoque <= 0)
                return BadRequest("Livro não disponível no estoque.");

            // Reduzir o estoque do livro
            livro.QuantidadeEstoque -= 1;
            _context.Entry(livro).State = EntityState.Modified;

            // Relacionar empréstimo ao usuário
            emprestimo.Usuarios = new List<Usuario> { usuario };
            _context.Emprestimos.Add(emprestimo);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = emprestimo.Id }, emprestimo);
        }

        // Atualizar um empréstimo (por exemplo, devolver o livro)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
                return BadRequest("ID do empréstimo inconsistente.");

            var emprestimoExistente = await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimoExistente == null)
                return NotFound($"Empréstimo #{id} não encontrado.");

            try
            {
                if (emprestimo.DataDevolucao != null && emprestimoExistente.DataDevolucao == null)
                {
                    // Atualizar estoque ao devolver o livro
                    var livro = emprestimoExistente.Livro;
                    if (livro != null)
                    {
                        livro.QuantidadeEstoque++;
                        _context.Entry(livro).State = EntityState.Modified;
                    }
                }

                emprestimoExistente.DataDevolucao = emprestimo.DataDevolucao;
                _context.Entry(emprestimoExistente).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Erro ao atualizar empréstimo: {ex.Message}");
            }
        }

        // Excluir um empréstimo
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
            {
                return NotFound($"Empréstimo #{id} não encontrado.");
            }

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
