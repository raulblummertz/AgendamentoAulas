using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces;

public interface IAlunoService
{
    Task<AlunoDto> ListarAlunoPorId(int id);
    Task<IEnumerable<AlunoDto>> ListarAlunos();
    Task CadastroAluno(AlunoDto alunoDto);
    Task EditarAluno(int id, AlunoDto alunoDto);
    Task ApagarAluno(int id);
}

