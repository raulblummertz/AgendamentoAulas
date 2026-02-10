using System.ComponentModel.DataAnnotations;

namespace Agendamento.Domain.Entities;

public class Aula
{
    [Key]
    public int Id { get; set; }
    public string TipoAula { get; set; }
    public DateTime DataHora { get; set; }
    public int CapacidadeMaxima { get; set; }

    public Aula(string tipoAula, DateTime dataHora, int capacidadeMaxima)
    {
        TipoAula = tipoAula;
        DataHora = dataHora;
        CapacidadeMaxima = capacidadeMaxima;
    }
}

