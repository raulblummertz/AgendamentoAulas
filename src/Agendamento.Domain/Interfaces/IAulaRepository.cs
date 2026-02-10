using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces;

public interface IAulaRepository
{
    Task<Aula?> GetByIdAsync(int id);
    Task<IEnumerable<Aula>> GetAllAsync();
    Task AddAsync(Aula aula);
    Task UpdateAsync(Aula aula);
    Task DeleteAsync(int id);
}
