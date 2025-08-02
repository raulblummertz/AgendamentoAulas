using Agendamento01.Alunos.DTO;

namespace Agendamento01.Alunos.Interfaces
{
    public interface IAlunoService
    {
        Task CadastroAluno(AlunoDto alunoDto);
    }
}
