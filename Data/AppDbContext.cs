using Microsoft.EntityFrameworkCore;
using GestorTarefas.Models;

namespace GestorTarefas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<HistoricoTarefa> HistoricosTarefa { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistoricoTarefa>()
            .HasOne(h => h.Tarefa)
            .WithMany(t => t.Historicos)
            .HasForeignKey(h => h.TarefaId);
    }
}
