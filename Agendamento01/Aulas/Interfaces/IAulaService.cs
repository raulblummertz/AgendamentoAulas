using Agendamento01.Alunos.Model;
using Agendamento01.Aulas.DTO;
using Agendamento01.Aulas.Model;
using Microsoft.EntityFrameworkCore;

namespace Agendamento01.Aulas.Interfaces
{
    public interface IAulaService
    {
        Task Agendamento(int alunoId, int aulaId);
        Task CadastroAula(AulaDto aulaDto);
    }
}
