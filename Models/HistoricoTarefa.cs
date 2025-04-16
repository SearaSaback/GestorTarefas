namespace GestorTarefas.Models;

public class HistoricoTarefa
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public string CampoAlterado { get; set; } = string.Empty;
    public string ValorAnterior { get; set; } = string.Empty;
    public string NovoValor { get; set; } = string.Empty;
    public DateTime DataAlteracao { get; set; } = DateTime.Now;
    public string Usuario { get; set; } = "Sistema";

    public Tarefa? Tarefa { get; set; }
}