using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Application.Services;

public class AlunoService : IServiceBase<AlunoDto>
{
    private readonly IRepositoryBase<Aluno> _repository;

    public AlunoService(IRepositoryBase<Aluno> repository)
    {
        _repository = repository;
    }

    public async Task<AlunoDto> ListarPorId(int id)
    {
        var aluno = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Aluno não encontrado.");

        return new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Plano = aluno.Plano
        };
    }

    public async Task<IEnumerable<AlunoDto>> ListarTodos()
    {
        var alunos = await _repository.GetAllAsync();

        return alunos.Select(a => new AlunoDto
        {
            Id = a.Id,
            Nome = a.Nome,
            Plano = a.Plano
        });
    }

    public async Task Cadastrar(AlunoDto alunoDto)
    {

        if (string.IsNullOrWhiteSpace(alunoDto.Nome))
            throw new ArgumentException("Nome do aluno não pode ser vazio.");

        var novoAluno = new Aluno(alunoDto.Nome, alunoDto.Plano);
        await _repository.AddAsync(novoAluno);
    }

    public async Task Editar(int id, AlunoDto alunoDto)
    {
        var alunoExistente = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Aluno não encontrado.");;
        if (string.IsNullOrWhiteSpace(alunoDto.Nome))
            throw new ArgumentException("Nome do aluno não pode ser vazio.");
        alunoExistente.Nome = alunoDto.Nome;
        alunoExistente.Plano = alunoDto.Plano;
        await _repository.UpdateAsync(alunoExistente);

    }

    public async Task Apagar(int id)
    {
        var alunoExistente = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Aluno não encontrado.");
        await _repository.DeleteAsync(alunoExistente.Id);
    }
}
