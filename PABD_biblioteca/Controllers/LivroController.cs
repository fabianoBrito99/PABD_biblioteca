using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;
using System.Threading.Tasks;

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
            var livros = await _context.Livros.Include(l => l.Estoque).ToListAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _context.Livros.Include(l => l.Estoque).FirstOrDefaultAsync(l => l.Id == id);
            if (livro == null)
                return NotFound("Livro não encontrado.");

            return Ok(livro);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivroDto livroDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = new Livro
            {
                Nome = livroDto.Nome,
                QuantidadePaginas = livroDto.QuantidadePaginas,
                Descricao = livroDto.Descricao,
                AnoPublicacao = livroDto.AnoPublicacao
            };

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync(); // Salvar o livro primeiro para obter o ID

            // Criar os Autores (caso não existam)
            foreach (var autorNome in livroDto.AutoresNomes)
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Nome == autorNome);
                if (autor == null)
                {
                    autor = new Autor { Nome = autorNome };
                    _context.Autores.Add(autor);
                    await _context.SaveChangesAsync();
                }
                _context.AutorLivros.Add(new AutorLivro { AutorId = autor.Id, LivroId = livro.Id });
            }

            // Criar as Categorias (caso não existam)
            foreach (var categoriaNome in livroDto.CategoriasNomes)
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == categoriaNome);
                if (categoria == null)
                {
                    categoria = new Categoria { Nome = categoriaNome };
                    _context.Categorias.Add(categoria);
                    await _context.SaveChangesAsync();
                }
                _context.LivroCategorias.Add(new LivroCategoria { CategoriaId = categoria.Id, LivroId = livro.Id });
            }

            // Criar o estoque associado ao livro
            var estoque = new Estoque
            {
                LivroId = livro.Id,
                Quantidade = livroDto.QuantidadeEstoque
            };

            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = livro.Id }, livro);
        }



        [HttpPut("estoque/{livroId}")]
        public async Task<IActionResult> AtualizarEstoque(int livroId, [FromBody] int quantidade)
        {
            var estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.LivroId == livroId);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            estoque.Quantidade = quantidade;
            await _context.SaveChangesAsync();

            return Ok(estoque);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LivroDto livroDto)
        {
            var livro = await _context.Livros.Include(l => l.Estoque).FirstOrDefaultAsync(l => l.Id == id);
            if (livro == null)
                return NotFound($"Livro #{id} não encontrado.");

            // Preencher automaticamente os dados do livro
            livro.Nome = livroDto.Nome ?? livro.Nome;
            livro.QuantidadePaginas = livroDto.QuantidadePaginas ?? livro.QuantidadePaginas;
            livro.Descricao = livroDto.Descricao ?? livro.Descricao;
            livro.AnoPublicacao = livroDto.AnoPublicacao ?? livro.AnoPublicacao;

            // Atualizar estoque associado ao livro
            var estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.LivroId == id);
            if (estoque != null)
            {
                estoque.Quantidade = livroDto.QuantidadeEstoque;
                _context.Entry(estoque).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Livro atualizado com sucesso.",
                LivroAtualizado = livro
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _context.Livros.Include(l => l.Estoque).FirstOrDefaultAsync(l => l.Id == id);
            if (livro == null)
                return NotFound($"Livro #{id} não encontrado.");

            // Capturar os dados antes de excluir
            var livroExcluido = new
            {
                Id = livro.Id,
                Nome = livro.Nome,
                QuantidadePaginas = livro.QuantidadePaginas,
                Descricao = livro.Descricao,
                AnoPublicacao = livro.AnoPublicacao,
                Estoque = livro.Estoque != null ? livro.Estoque.Quantidade : 0
            };

            // Remover o estoque antes do livro
            var estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.LivroId == id);
            if (estoque != null)
            {
                _context.Estoques.Remove(estoque);
                await _context.SaveChangesAsync();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Livro excluído com sucesso.",
                LivroExcluido = livroExcluido
            });
        }


    }
}
