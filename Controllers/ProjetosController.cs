using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTarefas.Data;
using GestorTarefas.Models;

namespace GestorTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetAll()
        {
            return await _context.Projetos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Projeto>> Create(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = projeto.Id }, projeto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projeto = await _context.Projetos
                .Include(p => p.Tarefas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projeto == null)
                return NotFound("Projeto não encontrado.");

            bool possuiTarefasPendentes = projeto.Tarefas.Any(t => t.Status == "Pendente");

            if (possuiTarefasPendentes)
            {
                return BadRequest("O projeto possui tarefas pendentes. Conclua ou remova as tarefas antes de excluir o projeto.");
            }

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}