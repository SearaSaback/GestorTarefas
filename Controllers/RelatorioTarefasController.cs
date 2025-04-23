using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTarefas.Data;
using GestorTarefas.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestorTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioTarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatorioTarefasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "gerente")]
        [HttpGet("relatorio/desempenho")]
        public async Task<IActionResult> GetRelatorioDesempenho([FromQuery] int dias = 30)
        {
            var dataInicio = DateTime.UtcNow.AddDays(-dias);

            var desempenho = await _context.Tarefas
                .Where(t => t.Status == "Concluída" && t.DataConclusao >= dataInicio)
                .GroupBy(t => t.UsuarioId)
                .Join(_context.Usuarios,
                      grupo => grupo.Key,
                      usuario => usuario.Id,
                      (grupo, usuario) => new
                      {
                          UsuarioId = usuario.Id,
                          Nome = usuario.Nome,
                          TotalConcluidas = grupo.Count(),
                          MediaPorDia = Math.Round(grupo.Count() / (double)dias, 2)
                      })
                .OrderByDescending(r => r.MediaPorDia)
                .ToListAsync();

            return Ok(new
            {
                Periodo = $"Últimos {dias} dias",
                GeradoEm = DateTime.UtcNow,
                TotalUsuarios = desempenho.Count,
                Resultados = desempenho
            });
        }
    }
}