using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infrastructure.Data;



namespace Agendamento.Infrastructure.Repositories;

public class AulaRepository : RepositoryBase<Aula>
{
    public AulaRepository(AgendamentoContext context) : base(context)
    {

    }
}
