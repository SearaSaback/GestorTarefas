

namespace GestorTarefas.Controllers
{
    [HttpGet("relatorios/desempenho")]
    [Authorize(Roles = "gerente")]
    public async Task<IActionResult> GetRelatorioDesempenho()
    {
        var trintaDiasAtras = DateTime.UtcNow.AddDays(-30);

        var desempenho = await _context.Usuarios
            .Select(u => new
            {
                Usuario = u.Nome,
                MediaTarefasConcluidas = u.Tarefas
                    .Where(t => t.Status == "Concluída" && t.DataConclusao >= trintaDiasAtras)
                    .Count() / 30.0
            })
            .ToListAsync();

        return Ok(desempenho);
    }
}