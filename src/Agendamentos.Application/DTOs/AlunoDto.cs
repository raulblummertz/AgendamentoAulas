using Agendamento.Domain.Enums;

namespace Agendamento.Application.DTOs;

public class AlunoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public EnumTipoPlano Plano { get; set; }

}

