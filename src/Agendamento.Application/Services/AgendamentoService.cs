using System;
using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Application.Services;

public class AgendamentoService : IAgendamentoService
{

    private readonly IAlunoRepository _alunoRepository;
    private readonly IAulaRepository _aulaRepository;
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AgendamentoService(IAlunoRepository alunoRepository, IAulaRepository aulaRepository, IAgendamentoRepository agendamentoRepository)
    {
        _alunoRepository = alunoRepository;
        _aulaRepository = aulaRepository;
        _agendamentoRepository = agendamentoRepository;
    }

    public async Task AddAgendamento(int alunoId, int aulaId)
    {
        var aluno = await _alunoRepository.GetByIdAsync(alunoId);
        var aula = await _aulaRepository.GetByIdAsync(aulaId);
        var aulasAgendadasMes = await _agendamentoRepository.CountAgendamentosMesAsync(alunoId,aulaId);

        var participantesAtuais = await _agendamentoRepository.CountParticipantesAula(aulaId);

        if (aluno == null) throw new KeyNotFoundException("Aluno não encontrado.");
        if (aula == null) throw new KeyNotFoundException("Aula não encontrada.");
        if (aluno.LimiteAulas <= aulasAgendadasMes)
            throw new InvalidOperationException($"O aluno atingiu o limite de {aluno.LimiteAulas} aulas mensais do plano {aluno.Plano}.");
        if (participantesAtuais >= aula.CapacidadeMaxima)
            throw new InvalidOperationException("Aula já está cheia.");
        if (aula.DataHora < DateTime.Now)
            throw new InvalidOperationException("Aula já ocorreu ou está agendada para o passado.");


        var novoAgendamento = new Agendamentos(alunoId, aulaId);
        await _agendamentoRepository.AddAsync(novoAgendamento);
    }

    public async Task ApagarAgendamento(int id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        if (agendamento == null) throw new KeyNotFoundException("Agendamento não encontrado.");
        await _agendamentoRepository.DeleteAsync(id);
    }

    public async Task AtualizarAgendamento(int id, AgendamentoDto agendamento)
    {
        var existente = await _agendamentoRepository.GetByIdAsync(id);
        if (existente == null) throw new KeyNotFoundException("Agendamento não encontrado.");
        existente.AlunoId = agendamento.AlunoId;
        existente.AulaId = agendamento.AulaId;
        await _agendamentoRepository.UpdateAsync(existente);
    }

    public async Task<IEnumerable<AgendamentoDto>> ListarAgendamentos()
    {
        var agendamentos = await _agendamentoRepository.GetAllAsync();
        return agendamentos.Select(a => new AgendamentoDto
        {
            Id = a.Id,
            AlunoId = a.AlunoId,
            AulaId = a.AulaId,
            Aluno = a.Aluno,
            Aula = a.Aula
        });
    }

    public async Task<AgendamentoDto?> ListarAgendamentosPorId(int id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        return agendamento == null ? null : new AgendamentoDto
        {
            Id = agendamento.Id,
            AlunoId = agendamento.AlunoId,
            AulaId = agendamento.AulaId,
            Aluno = agendamento.Aluno,
            Aula = agendamento.Aula
        };
    }
}
