using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces;

public interface IAgendamentoRepository
{
    Task<Agendamentos?> GetByIdAsync(int id);
    Task<IEnumerable<Agendamentos>> GetAllAsync();
    Task<int> CountAgendamentosMesAsync(int alunoId, int aulaId);
    Task<int> CountParticipantesAula(int aulaId);
    Task<int> CountAulasMesAsync(int alunoId, DateTime dataInicio, DateTime dataFim);
    Task<List<(string TipoAula, int Quantidade)>> GetAulasMaisFrequentesAsync(int alunoId, DateTime dataInicio, DateTime dataFim);
    Task AddAsync(Agendamentos agendamento);
    Task UpdateAsync(Agendamentos agendamento);
    Task DeleteAsync(int id);
}