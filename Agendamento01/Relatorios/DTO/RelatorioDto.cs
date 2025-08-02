namespace Agendamento01.Relatorios.DTO
{
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
}
