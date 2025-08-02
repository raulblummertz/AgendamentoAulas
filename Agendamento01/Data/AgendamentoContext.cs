using Agendamento01.Alunos.Model;
using Agendamento01.Aulas.Model;
using Microsoft.EntityFrameworkCore;

namespace Agendamento01.Data
{
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
        public DbSet<Agendamento> Agendamento { get; set; }
    }
}
