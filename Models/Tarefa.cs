namespace GestorTarefas.Models;

public class Tarefa
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Detalhe { get; set; }
    public required string Status { get; set; }
    public required string Prioridade { get; set; }
    public int ProjetoId {get; set; }
    public int UsuarioId {get; set; }    
    public ICollection<HistoricoTarefa> Historicos { get; set; } = new List<HistoricoTarefa>();
}

