using Agendamento.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Agendamento.Domain.Factories;

namespace Agendamento.Domain.Entities;

public class Aluno
{
    private static PlanoStrategyFactory _factory;

    public static void ConfigurarFactory(PlanoStrategyFactory factory)
    {
        _factory = factory;
    }
    
    
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public EnumTipoPlano Plano { get; set; }
    public int LimiteAulas { get; set; }
    public Aluno(string nome, EnumTipoPlano plano)
    {
        Nome = nome;
        Plano = plano;
        switch (plano)
        {
            case EnumTipoPlano.Mensal:
                LimiteAulas = 12;
                break;
            case EnumTipoPlano.Trimestral:
                LimiteAulas = 20;
                break;
            case EnumTipoPlano.Anual:
                LimiteAulas = 30;
                break;
            default:
                throw new ArgumentException("Plano inválido");
        }
    }
    

    public void AlterarPlano(EnumTipoPlano plano)
    {
        if (_factory == null)
        {
            throw new InvalidOperationException("PlanoStrategyFactory não foi configurado");
        }
        var strategy = _factory.ObterPlano(plano);
        Plano = plano;
        LimiteAulas = strategy.CalcularLimiteAula();
    }
}

