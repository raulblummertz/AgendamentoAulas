using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.API.Validators;

public class AlunoDtoValidator : AbstractValidator<AlunoDto>
{
    public AlunoDtoValidator()
    {
        RuleFor(a => a.Nome)
            .NotEmpty().WithMessage("O nome do aluno � obrigat�rio.");
        RuleFor(a => a.Plano)
            .NotEmpty().WithMessage("O tipo do plano � obrigat�rio.");
    }
}