using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;

namespace PABD_biblioteca.Controllers
{
    [ApiController]
    [Route("Livros")]
    public class LivroController : Controller
    {

        private readonly AppDbContext _context;

        public LivroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listaServidores = await _context.Livros.ToListAsync();

                return Ok(listaServidores);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var servidor = await _context.Livros.Where(s => s.Id == id).FirstOrDefaultAsync();

                if (servidor == null)
                {
                    return NotFound($"Servidor #{id} não encontrado");
                }

                return Ok(servidor);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LivroDto item)
        {
            try
            {
                var livro = new Livro()
                {
                    Nome = item.Nome,
                   
                };

                await _context.Livros.AddAsync(livro);
                await _context.SaveChangesAsync();

                return Created("", livro);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LivroDto item)
        {
            try
            {
                var livro = await _context.Livros.FindAsync(id);

                if (livro is null)
                {
                    return NotFound();
                }

                livro.Nome = item.Nome;
              

                _context.Livros.Update(livro);
                await _context.SaveChangesAsync();

                return Ok(livro);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var livro = await _context.Livros.FindAsync(id);

                if (livro is null)
                {
                    return NotFound();
                }

                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}