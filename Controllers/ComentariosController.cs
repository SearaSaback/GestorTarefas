using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTarefas.Data;
using GestorTarefas.Models;

namespace GestorTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComentariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("tarefa/{tarefaId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetAllByTarefa(int tarefaId)
        {
            var comentarios = await _context.Comentarios
            .Where(c => c.TarefaId == tarefaId)
            .OrderByDescending(c => c.DataCriacao)
            .Select(c => new
            {
                c.Id,
                c.Texto,
                c.DataCriacao,
                Usuario = c.Usuario!.Nome
            })
            .ToListAsync();

            if (!comentarios.Any())
                return NotFound("Nenhum comentário encontrado para essa tarefa.");

            return Ok(comentarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comentario == null)
                return NotFound("Comentário não encontrado.");

            return Ok(new
            {
                comentario.Id,
                comentario.Texto,
                comentario.DataCriacao,
                Usuario = comentario.Usuario?.Nome
            });
        }

        [HttpPost]
        public async Task<ActionResult<Comentario>> Create(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = comentario.Id }, comentario);
        }
    }
}