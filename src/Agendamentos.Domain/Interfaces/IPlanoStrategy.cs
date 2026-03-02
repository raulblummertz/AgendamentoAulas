using Agendamento.Domain.Enums;

namespace Agendamento.Domain.Interfaces;

public interface IPlanoStrategy
{
    int CalcularLimiteAula();
    EnumTipoPlano TipoPlano { get; }
}