using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _repository;

    public AlunoService(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task<AlunoDto> ListarAlunoPorId(int id)
    {
        var aluno = await _repository.GetByIdAsync(id);
        if (aluno == null)
            throw new KeyNotFoundException("Aluno não encontrado.");

        return new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Plano = aluno.Plano
        };
    }

    public async Task<IEnumerable<AlunoDto>> ListarAlunos()
    {
        var alunos = await _repository.GetAllAsync();

        return alunos.Select(a => new AlunoDto
        {
            Id = a.Id,
            Nome = a.Nome,
            Plano = a.Plano
        });


    }

    public async Task CadastroAluno(AlunoDto alunoDto)
    {

        if (string.IsNullOrWhiteSpace(alunoDto.Nome))
            throw new ArgumentException("Nome do aluno não pode ser vazio.");

        var novoAluno = new Aluno(alunoDto.Nome, alunoDto.Plano);
        await _repository.AddAsync(novoAluno);
    }

    public async Task EditarAluno(int id, AlunoDto alunoDto)
    {
        var alunoExistente = await _repository.GetByIdAsync(id);
        if (alunoExistente == null)
            throw new KeyNotFoundException("Aluno não encontrado.");
        if (string.IsNullOrWhiteSpace(alunoDto.Nome))
            throw new ArgumentException("Nome do aluno não pode ser vazio.");
        alunoExistente.Nome = alunoDto.Nome;
        alunoExistente.Plano = alunoDto.Plano;
        await _repository.UpdateAsync(alunoExistente);
    }

    public async Task ApagarAluno(int id)
    {
        var alunoExistente = await _repository.GetByIdAsync(id);
        if (alunoExistente == null)
            throw new KeyNotFoundException("Aluno não encontrado.");
        await _repository.DeleteAsync(id);
    }
}
