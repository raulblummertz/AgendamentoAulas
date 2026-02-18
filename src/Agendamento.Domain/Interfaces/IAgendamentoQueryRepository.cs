namespace Agendamento.Domain.Interfaces;

public interface IAgendamentoQueryRepository
{
    Task<int> CountAgendamentosMesAsync(int alunoId, int aulaId);
    Task<int> CountParticipantesAula(int aulaId);
    Task<int> CountAulasMesAsync(int alunoId, DateTime dataInicio, DateTime dataFim);
    Task<List<(string TipoAula, int Quantidade)>> GetAulasMaisFrequentesAsync(int alunoId, DateTime dataInicio, DateTime dataFim);
}