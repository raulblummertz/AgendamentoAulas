using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.API.Validators;

public class AlunoDtoValidator : AbstractValidator<AlunoDto>
{
    public AlunoDtoValidator()
    {
        RuleFor(a => a.Nome)
            .NotEmpty().WithMessage("O nome do aluno é obrigatório.")
        RuleFor(a => a.TipoPlano)
            .NotEmpty().WithMessage("O tipo do plano é obrigatório.")
    }
}