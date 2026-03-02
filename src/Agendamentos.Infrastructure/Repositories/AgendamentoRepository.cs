using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Agendamento.Infrastructure.Repositories;

public class AgendamentoRepository : RepositoryBase<Domain.Entities.Agendamento>
{
    public AgendamentoRepository(AgendamentoContext context) : base(context)
    {
    }
}
