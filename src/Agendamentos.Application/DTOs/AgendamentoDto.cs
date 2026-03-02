namespace Agendamento.Application.DTOs;
using Agendamento.Domain.Entities;

public class AgendamentoDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int AulaId { get; set; }
    public Aluno Aluno { get; set; }
    public Aula Aula { get; set; }
}
