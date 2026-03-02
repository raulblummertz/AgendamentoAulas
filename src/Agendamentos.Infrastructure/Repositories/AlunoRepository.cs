using Agendamento.Domain.Entities;
using Agendamento.Infrastructure.Data;

namespace Agendamento.Infrastructure.Repositories;

public class AlunoRepository : RepositoryBase<Aluno>
{
    private readonly AgendamentoContext _context;

    public AlunoRepository(AgendamentoContext context) : base(context)
    {
    }
}
