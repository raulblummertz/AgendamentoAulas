using Agendamento.Domain.Entities;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces;

public interface IAulaService
{
    Task CadastroAula(AulaDto aulaDto);
    Task EditarAula(int id, AulaDto aulaDto);
    Task ApagarAula(int id);
    Task<AulaDto?> ListarAulaPorId(int id);
    Task<IEnumerable<AulaDto>> ListarAulas();
}

