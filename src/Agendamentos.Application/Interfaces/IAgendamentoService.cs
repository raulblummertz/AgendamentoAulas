using System;
using Agendamento.Domain.Entities;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces;

public interface IAgendamentoService : IServiceBase<AgendamentoDto>
{
    Task Cadastrar(int alunoId, int aulaId);
}
