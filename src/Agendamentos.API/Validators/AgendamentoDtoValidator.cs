using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.API.Validators;

public class AgendamentoDtoValidator : AbstractValidator<AgendamentoDto>
{
    public AgendamentoDtoValidator()
    {
        RuleFor(a => a.AlunoId)
            .NotNull().WithMessage("O ID do aluno deve ser informado.");
        RuleFor(a => a.AulaId)
            .NotNull().WithMessage("O ID da aula deve ser informado.");
    }
}