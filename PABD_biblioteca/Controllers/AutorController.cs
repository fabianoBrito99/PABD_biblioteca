using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;

namespace PABD_biblioteca.Controllers
{
    [ApiController]
    [Route("Autores")]
    public class AutorController : Controller
    {
        private readonly AppDbContext _context;

        public AutorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var autores = await _context.Autores.ToListAsync();
                return Ok(autores);
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
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound($"Autor #{id} não encontrado.");
                }

                return Ok(autor);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Autor autor)
        {
            try
            {
                var novoAutor = new Autor { Nome = autor.Nome };
                _context.Autores.Add(novoAutor);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = novoAutor.Id }, new { novoAutor.Id, novoAutor.Nome });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Autor autor)
        {
            if (autor.Id == 0) 
            {
                autor.Id = id;
            }

            if (id != autor.Id) 
            {
                return BadRequest("ID do autor inconsistente.");
            }

            var autorExistente = await _context.Autores.FindAsync(id);
            if (autorExistente == null)
            {
                return NotFound($"Autor #{id} não encontrado.");
            }

            try
            {
                autorExistente.Nome = autor.Nome; 
                await _context.SaveChangesAsync();
                return NoContent();
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
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound($"Autor #{id} não encontrado.");
                }

                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
