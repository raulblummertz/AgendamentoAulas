using Agendamento.Domain.Enums;
using Agendamento.Domain.Interfaces;


namespace Agendamento.Domain.Strategies;

public class PlanoTrimestralStrategy : IPlanoStrategy
{
    public int CalcularLimiteAula() => 20;
    public EnumTipoPlano TipoPlano => EnumTipoPlano.Trimestral;
}