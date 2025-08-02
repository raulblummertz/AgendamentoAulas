using Agendamento01.Alunos.DTO;
using Agendamento01.Alunos.Model;
using Agendamento01.Data;

namespace Agendamento01.Alunos.Services
{
    public class AlunoService : Interfaces.IAlunoService
    {
        private readonly AgendamentoContext _context;

        public AlunoService(AgendamentoContext context)
        {
            _context = context;
        }
        public async Task CadastroAluno(AlunoDto alunoDto)
        {
            
            if (string.IsNullOrWhiteSpace(alunoDto.Nome))
                throw new ArgumentException("Nome do aluno não pode ser vazio.");

            var novoAluno = new Aluno(alunoDto.Nome, alunoDto.Plano);
            _context.Alunos.Add(novoAluno);
            await _context.SaveChangesAsync();
        }
    }
}
