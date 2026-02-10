using Agendamento.Domain.Interfaces;
using Agendamento.Application.DTOs;
using static Agendamento.Application.DTOs.RelatorioDto;

namespace Agendamento.Application.Services;

public class RelatorioService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IAgendamentoRepository _agendamentoRepository;

    public RelatorioService(IAlunoRepository alunoRepository, IAgendamentoRepository agendamentoRepository)
    {
        _alunoRepository = alunoRepository;
        _agendamentoRepository = agendamentoRepository;
    }
    public async Task<RelatorioDto> RelatorioAulas(int alunoId)
    {
        var hoje = DateTime.UtcNow;
        var aluno = await _alunoRepository.GetByIdAsync(alunoId);
        var primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
        var aulasAgendadasMes = await _agendamentoRepository.CountAulasMesAsync(alunoId, primeiroDiaDoMes, primeiroDiaDoProximoMes);

        var frequenciaAulas = await _agendamentoRepository.GetAulasMaisFrequentesAsync(alunoId, primeiroDiaDoMes, primeiroDiaDoProximoMes);

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

