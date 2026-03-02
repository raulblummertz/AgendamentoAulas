using Agendamento.Domain.Enums;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Domain.Strategies;

public class PlanoAnualStrategy : IPlanoStrategy
{
    public int CalcularLimiteAula() => 30;
    public EnumTipoPlano TipoPlano => EnumTipoPlano.Anual;
}