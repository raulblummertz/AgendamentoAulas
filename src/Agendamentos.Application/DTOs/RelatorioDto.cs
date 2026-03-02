namespace Agendamento.Application.DTOs;

    public class RelatorioDto
{
    public class FrequenciaAulasDto
    {
        public string TipoAula { get; set; }
        public int Quantidade { get; set; }
    }
    public int TotalAulaMes { get; set; }
    public List<FrequenciaAulasDto> AulaMaisFrequente { get; set; }
}

