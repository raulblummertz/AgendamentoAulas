using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Application.Services;

public class AulaService : IAulaService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IAulaRepository _aulaRepository;
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AulaService(IAlunoRepository alunoRepository, IAulaRepository aulaRepository, IAgendamentoRepository agendamentoRepository)
    {
        _alunoRepository = alunoRepository;
        _aulaRepository = aulaRepository;
        _agendamentoRepository = agendamentoRepository;
    }

    public async Task CadastroAula(AulaDto aulaDto)
    {
        if (string.IsNullOrWhiteSpace(aulaDto.TipoAula))
            throw new ArgumentException("Tipo de aula não pode ser vazio.");

        var novaAula = new Aula(aulaDto.TipoAula, aulaDto.DataHora.ToUniversalTime(), aulaDto.CapacidadeMaxima);
        await _aulaRepository.AddAsync(novaAula);
    }

    public async Task EditarAula(int id, AulaDto aulaDto)
    {
        var aulaExistente = await _aulaRepository.GetByIdAsync(id);
        if (aulaExistente == null) throw new KeyNotFoundException("Aula não encontrada.");
        aulaExistente.TipoAula = aulaDto.TipoAula;
        aulaExistente.DataHora = aulaDto.DataHora.ToUniversalTime();
        aulaExistente.CapacidadeMaxima = aulaDto.CapacidadeMaxima;
        await _aulaRepository.UpdateAsync(aulaExistente);
    }

    public async Task ApagarAula(int id)
    {
        var aulaExistente = await _aulaRepository.GetByIdAsync(id);
        if (aulaExistente == null) throw new KeyNotFoundException("Aula não encontrada.");
        await _aulaRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AulaDto>> ListarAulas()
    {
        var aulas = await _aulaRepository.GetAllAsync();
        return aulas.Select(a => new AulaDto
        {
            Id = a.Id,
            TipoAula = a.TipoAula,
            DataHora = a.DataHora,
            CapacidadeMaxima = a.CapacidadeMaxima
        });
    }

    public async Task<AulaDto?> ListarAulaPorId(int id)
    {
        var aula = await _aulaRepository.GetByIdAsync(id);
        return aula == null ? null : new AulaDto
        {
            Id = aula.Id,
            TipoAula = aula.TipoAula,
            DataHora = aula.DataHora,
            CapacidadeMaxima = aula.CapacidadeMaxima
        };
    }
}
