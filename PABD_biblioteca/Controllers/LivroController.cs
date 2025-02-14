using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;

namespace PABD_biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LivroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _context.Livros.ToListAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
                return NotFound("Livro não encontrado.");

            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivroDto livroDto)
        {
            var livro = new Livro
            {
                Nome = livroDto.Nome,
                QuantidadePaginas = livroDto.QuantidadePaginas,
                Descricao = livroDto.Descricao,
                AnoPublicacao = livroDto.AnoPublicacao,
                QuantidadeEstoque = livroDto.QuantidadeEstoque
            };

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = livro.Id }, livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LivroDto livroDto)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
                return NotFound("Livro não encontrado.");

            livro.Nome = livroDto.Nome;
            livro.QuantidadePaginas = livroDto.QuantidadePaginas;
            livro.Descricao = livroDto.Descricao;
            livro.AnoPublicacao = livroDto.AnoPublicacao;
            livro.QuantidadeEstoque = livroDto.QuantidadeEstoque;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
                return NotFound("Livro não encontrado.");

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
