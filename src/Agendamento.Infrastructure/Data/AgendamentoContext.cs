using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infrastructure.Data;

public class AgendamentoContext : DbContext
{
    public AgendamentoContext(DbContextOptions<AgendamentoContext> options) : base(options)
    {
    }
    /* protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         base.OnModelCreating(modelBuilder);
     }
    */
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Agendamentos> Agendamentos { get; set; }
}

