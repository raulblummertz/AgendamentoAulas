using Agendamento01.Alunos.Model;

namespace Agendamento01.Aulas.Model
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int AulaId { get; set; }
        public Aluno Aluno { get; set; }
        public Aula Aula { get; set; }

        public Agendamento(int alunoId, int aulaId)
        {
            AlunoId = alunoId;
            AulaId = aulaId;
        }
    }
}
