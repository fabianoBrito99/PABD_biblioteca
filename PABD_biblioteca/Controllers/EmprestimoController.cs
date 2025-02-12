using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
        {
            return await _context.Emprestimos.Include(e => e.Livro).ToListAsync();
        }



        [HttpPost]
        public async Task<ActionResult> CreateEmprestimo([FromBody] EmprestimoDto emprestimoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = await _context.Livros.FindAsync(emprestimoDto.LivroId);
            if (livro == null)
                return BadRequest("Livro não encontrado.");

            var emprestimo = new Emprestimo
            {
                LivroId = emprestimoDto.LivroId,
                DataEmprestimo = emprestimoDto.DataEmprestimo,
                DataPrevistaDevolucao = emprestimoDto.DataPrevistaDevolucao
            };

            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();

            foreach (var usuarioId in emprestimoDto.UsuariosIds)
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId);
                if (usuario == null)
                    return BadRequest($"Usuário com ID {usuarioId} não encontrado.");

                _context.UsuarioEmprestimos.Add(new UsuarioEmprestimo
                {
                    UsuarioId = usuarioId,
                    EmprestimoId = emprestimo.Id
                });
            }

            await _context.SaveChangesAsync();

            // 🔹 Recarregar o empréstimo com as relações carregadas corretamente
            var emprestimoCriado = await _context.Emprestimos
                .Include(e => e.Livro)
                .Include(e => e.UsuarioEmprestimos)
                .ThenInclude(ue => ue.Usuario)
                .FirstOrDefaultAsync(e => e.Id == emprestimo.Id);

            if (emprestimoCriado == null)
                return StatusCode(500, "Erro ao carregar empréstimo após criação.");

            return CreatedAtAction(nameof(GetEmprestimo), new { id = emprestimoCriado.Id }, new
            {
                emprestimoCriado.Id,
                emprestimoCriado.DataEmprestimo,
                emprestimoCriado.DataPrevistaDevolucao,
                emprestimoCriado.LivroId
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimos
                .Include(e => e.Livro) // 🔹 Carregar detalhes do livro
                .Include(e => e.UsuarioEmprestimos)
                .ThenInclude(ue => ue.Usuario) // 🔹 Carregar detalhes dos usuários
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound($"Empréstimo com ID {id} não encontrado.");

            return Ok(emprestimo); // 🔹 Retorna 200 OK corretamente quando o empréstimo é encontrado
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmprestimo(int id, [FromBody] EmprestimoDto emprestimoDto)
        {
            // 🔹 Buscar o empréstimo corretamente pelo ID
            var emprestimoExistente = await _context.Emprestimos.FindAsync(id);
            if (emprestimoExistente == null)
                return NotFound($"Empréstimo com ID {id} não encontrado.");

            // 🔹 Atualizar apenas os campos necessários
            emprestimoExistente.DataEmprestimo = emprestimoDto.DataEmprestimo;
            emprestimoExistente.DataPrevistaDevolucao = emprestimoDto.DataPrevistaDevolucao;
            emprestimoExistente.DataDevolucao = emprestimoDto.DataDevolucao;

            await _context.SaveChangesAsync();

            return NoContent(); // 🔹 Retornar 204 No Content quando a atualização for bem-sucedida
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
                return NotFound("Empréstimo não encontrado.");

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
