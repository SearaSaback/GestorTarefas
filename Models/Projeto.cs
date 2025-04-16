namespace GestorTarefas.Models;

public class Projeto
{
    public int Id { get; set; }
    public required string Nome { get; set; }    
    public int UsuarioId {get; set; }    
    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}

