namespace Agendamento.Application.DTOs;

public class AulaDto
{
    public int Id { get; set; }
    public string TipoAula { get; set; }
    public DateTime DataHora { get; set; }
    public int CapacidadeMaxima { get; set; }
}

