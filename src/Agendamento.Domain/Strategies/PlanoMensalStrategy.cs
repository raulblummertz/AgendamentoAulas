using Agendamento.Domain.Enums;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Domain.Strategies;

public class PlanoMensalStrategy : IPlanoStrategy
{
    public int CalcularLimiteAula() => 12;
    public EnumTipoPlano TipoPlano => EnumTipoPlano.Mensal;
    
}