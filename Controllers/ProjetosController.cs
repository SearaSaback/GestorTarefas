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
        public async Task<ActionResult<IEnumerable<Projeto>>> Get()
        {
            return await _context.Projetos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Projeto>> Post(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = projeto.Id }, projeto);
        }
    }
}