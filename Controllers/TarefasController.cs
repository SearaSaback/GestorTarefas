using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTarefas.Data;
using GestorTarefas.Models;

namespace GestorTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("projeto/{projetoId}")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> Get(int projetoId)
    {
        var projeto = await _context.Projetos.Include(p => p.Tarefas)
                                             .FirstOrDefaultAsync(p => p.Id == projetoId);
        if (projeto == null) return NotFound();

        return Ok(projeto.Tarefas); 
    }

        [HttpPost]
    public async Task<ActionResult<Tarefa>> Post(Tarefa tarefa)
    {
        var projeto = await _context.Projetos.FindAsync(tarefa.ProjetoId);
        if (projeto == null) return BadRequest("Projeto não encontrado");

       var tarefasDoProjeto = await _context.Tarefas
        .CountAsync(t => t.ProjetoId == tarefa.ProjetoId);

        if (tarefasDoProjeto >= 20)
        {
            return BadRequest("Limite máximo de 20 tarefas por projeto atingido.");
        }


        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { projetoId = tarefa.ProjetoId }, tarefa);
    }

         [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Tarefa tarefa)
    {
        var tarefaExistente = await _context.Tarefas.FindAsync(id);
    if (tarefaExistente == null) return NotFound();

    var historicos = new List<HistoricoTarefa>();

    if (tarefaExistente.Status != tarefa.Status)
    {
        historicos.Add(new HistoricoTarefa
        {
            TarefaId = id,
            CampoAlterado = "Status",
            ValorAnterior = tarefaExistente.Status,
            NovoValor = tarefa.Status,
            Usuario = "admin" 
        });

        tarefaExistente.Status = tarefa.Status;
    }

    if (tarefaExistente.Titulo != tarefa.Titulo)
    {
        historicos.Add(new HistoricoTarefa
        {
            TarefaId = id,
            CampoAlterado = "Título",
            ValorAnterior = tarefaExistente.Titulo,
            NovoValor = tarefa.Titulo,
            Usuario = "admin"
        });

        tarefaExistente.Titulo = tarefa.Titulo;
    }

    _context.HistoricosTarefa.AddRange(historicos);
    await _context.SaveChangesAsync();

    return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return NotFound();

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    }
}