using Agendamento.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Agendamento.Domain.Entities;

public class Aluno
{
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
}

