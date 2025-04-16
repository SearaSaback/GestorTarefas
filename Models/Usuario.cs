namespace GestorTarefas.Models;

public class Usuario
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Cargo { get; set; }
}
