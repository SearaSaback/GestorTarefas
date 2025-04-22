namespace GestorTarefas.Models;

public class Comentario
{
    public int Id { get; set; }
    public required string Texto { get; set; }
    public DateTime DataCriacao { get; set; }

    public int TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}