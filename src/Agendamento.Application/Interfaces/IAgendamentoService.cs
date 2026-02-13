using System;
using Agendamento.Domain.Entities;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces;

public interface IAgendamentoService
{
    Task<AgendamentoDto?> ListarAgendamentosPorId(int id);
    Task<IEnumerable<AgendamentoDto>> ListarAgendamentos();
    Task AddAgendamento(int alunoId, int aulaId);
    Task AtualizarAgendamento(int id, AgendamentoDto agendamento);
    Task ApagarAgendamento(int id);
}
