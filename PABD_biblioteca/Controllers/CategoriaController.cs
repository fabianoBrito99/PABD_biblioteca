﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PABD_biblioteca.DataContexts;
using PABD_biblioteca.Dtos;
using PABD_biblioteca.Models;

namespace PABD_biblioteca.Controllers
{
    [ApiController]
    [Route("Categorias")]
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // Listar todas as categorias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }

        // Obter uma categoria pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound($"Categoria com ID #{id} não encontrada.");
            }

            return Ok(categoria);
        }

        // Criar uma nova categoria
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categoria categoria)
        {
            try
            {
                var novaCategoria = new Categoria { Nome = categoria.Nome };
                _context.Categorias.Add(novaCategoria);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = novaCategoria.Id }, new { novaCategoria.Id, novaCategoria.Nome });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // Atualizar uma categoria existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                categoria.Id = id;
            }

            if (id != categoria.Id)
            {
                return BadRequest("ID da categoria inconsistente.");
            }

            var categoriaExistente = await _context.Categorias.FindAsync(id);
            if (categoriaExistente == null)
            {
                return NotFound($"Categoria #{id} não encontrada.");
            }

            try
            {
                categoriaExistente.Nome = categoria.Nome; 
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // Excluir uma categoria
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound($"Categoria com ID #{id} não encontrada.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
