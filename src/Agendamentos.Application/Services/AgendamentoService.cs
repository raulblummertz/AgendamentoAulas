using System;
using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Application.Services;

public class AgendamentoService : IAgendamentoService
{

    private readonly IRepositoryBase<Aluno> _alunoRepository;
    private readonly IRepositoryBase<Aula> _aulaRepository;
    private readonly IRepositoryBase<Domain.Entities.Agendamento> _agendamentoRepository;
    private readonly IAgendamentoQueryRepository _agendamentoQueryRepository;

    public AgendamentoService(IRepositoryBase<Aluno> alunoRepository, IRepositoryBase<Aula>  aulaRepository, IRepositoryBase<Domain.Entities.Agendamento> agendamentoRepository,  IAgendamentoQueryRepository agendamentoQueryRepository)
    {
        _alunoRepository = alunoRepository;
        _aulaRepository = aulaRepository;
        _agendamentoRepository = agendamentoRepository;
        _agendamentoQueryRepository = agendamentoQueryRepository;
    }

    public async Task Cadastrar(int alunoId, int aulaId)
    {
        var aluno = await _alunoRepository.GetByIdAsync(alunoId) ?? throw new KeyNotFoundException("Aluno não encontrado.");
        
        var aula = await _aulaRepository.GetByIdAsync(aulaId) ?? throw new KeyNotFoundException("Aula não encontrada.");
        if (aula.DataHora < DateTime.Now)
            throw new InvalidOperationException("Aula já ocorreu ou está agendada para o passado.");
        
        var aulasAgendadasMes = await _agendamentoQueryRepository.CountAgendamentosMesAsync(alunoId,aulaId);
        if (aluno.LimiteAulas <= aulasAgendadasMes)
            throw new InvalidOperationException($"O aluno atingiu o limite de {aluno.LimiteAulas} aulas mensais do plano {aluno.Plano}.");
        
        var participantesAtuais = await _agendamentoQueryRepository.CountParticipantesAula(aulaId);
        if (participantesAtuais >= aula.CapacidadeMaxima)
            throw new InvalidOperationException("Aula já está cheia.");
        
        var novoAgendamento = new Domain.Entities.Agendamento(alunoId, aulaId);
        await _agendamentoRepository.AddAsync(novoAgendamento);
    }

    public async Task Apagar(int id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        if (agendamento == null) throw new KeyNotFoundException("Agendamento não encontrado.");
        await _agendamentoRepository.DeleteAsync(id);
    }

    public Task Cadastrar(AgendamentoDto dto)
    {
        throw new NotSupportedException("Use Cadastrar(alunoId, aulaId)");
    }

    public async Task Editar(int id, AgendamentoDto agendamentoDto)
    {
        var agendamentoExistente = await _agendamentoRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Agendamento não encontrado.");
        agendamentoExistente.AlunoId = agendamentoDto.AlunoId;
        agendamentoExistente.AulaId = agendamentoDto.AulaId;
        await _agendamentoRepository.UpdateAsync(agendamentoExistente);
    }

    public async Task<IEnumerable<AgendamentoDto>> ListarTodos()
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

    public async Task<AgendamentoDto?> ListarPorId(int id)
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
