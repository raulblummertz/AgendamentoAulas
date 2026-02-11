using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.API.Validators;

public class AulaDtoValidator : AbstractValidator<AulaDto>
{
    public AulaDtoValidator()
    {
        RuleFor(a => a.Nome)
            .NotEmpty().WithMessage("O nome da aula é obrigatório.")
        RuleFor(a => a.TipoAula)
            .NotEmpty().WithMessage("O tipo da aula é obrigatório.")
        RuleFor(a => a.DataHora)
            .GreaterThan(DateTime.Now).WithMessage("A data e hora da aula devem ser no futuro.")
        RuleFor(a => a.CapacidadeMaxima)
            .GreaterThan(0).WithMessage("A capacidade máxima deve ser maior que zero.")
    }
}