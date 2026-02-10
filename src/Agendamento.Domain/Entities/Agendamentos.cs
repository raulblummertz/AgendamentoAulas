using System.ComponentModel.DataAnnotations;

namespace Agendamento.Domain.Entities;

public class Agendamentos
{
    [Key]
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int AulaId { get; set; }
    public Aluno Aluno { get; set; }
    public Aula Aula { get; set; }

    public Agendamentos(int alunoId, int aulaId)
    {
        AlunoId = alunoId;
        AulaId = aulaId;
    }
}

