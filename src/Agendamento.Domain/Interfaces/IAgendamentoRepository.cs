using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces;

public interface IAgendamentoRepository
{
    Task<Agendamentos?> GetByIdAsync(int id);
    Task<IEnumerable<Agendamentos>> GetAllAsync();
    Task AddAsync(Agendamentos agendamento);
    Task UpdateAsync(Agendamentos agendamento);
    Task DeleteAsync(int id);
}