using Agendamento.Domain.Enums;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Domain.Factories;

public class PlanoStrategyFactory
{
    private readonly Dictionary<EnumTipoPlano, IPlanoStrategy> _planoStrategies = new ();

    public PlanoStrategyFactory(IEnumerable<IPlanoStrategy> planoStrategies)
    {
        foreach (var planoStrategy in planoStrategies)
        {
            _planoStrategies.Add(planoStrategy.TipoPlano, planoStrategy);
        }
    }

    public IPlanoStrategy ObterPlano(EnumTipoPlano tipoPlano)
    {
        return !_planoStrategies.TryGetValue(tipoPlano, out var strategy) ? throw new ArgumentException($"Plano inválido: {tipoPlano}") : strategy;
    }
    
}