using Agendamento.Domain.Interfaces;
using Agendamento.Application.DTOs;
using static Agendamento.Application.DTOs.RelatorioDto;

namespace Agendamento.Application.Services;

public class RelatorioService
{
    
    private readonly IAgendamentoQueryRepository _agendamentoQueryRepository;

    public RelatorioService(IAgendamentoQueryRepository agendamentoQueryRepository)
    {
        _agendamentoQueryRepository = agendamentoQueryRepository;
    }
    public async Task<RelatorioDto> RelatorioAulas(int alunoId)
    {
        var hoje = DateTime.UtcNow;
        var primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
        var aulasAgendadasMes = await _agendamentoQueryRepository.CountAulasMesAsync(alunoId, primeiroDiaDoMes, primeiroDiaDoProximoMes);

        var frequenciaAulas = await _agendamentoQueryRepository.GetAulasMaisFrequentesAsync(alunoId, primeiroDiaDoMes, primeiroDiaDoProximoMes);

        var aulasMaisFrequentes = frequenciaAulas.Select(f => new FrequenciaAulasDto
        {
            TipoAula = f.TipoAula,
            Quantidade = f.Quantidade
        }).ToList();

        var relatorio = new RelatorioDto
        {
            TotalAulaMes = aulasAgendadasMes,
            AulaMaisFrequente = aulasMaisFrequentes
        };

        return relatorio;
    }
}

