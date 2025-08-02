using Agendamento01.Data;
using Agendamento01.Relatorios.DTO;
using Microsoft.EntityFrameworkCore;
using static Agendamento01.Relatorios.DTO.RelatorioDto;

namespace Agendamento01.Relatorios.Services
{
    public class RelatorioService
    {
        private readonly AgendamentoContext _context;

        public RelatorioService(AgendamentoContext context)
        {
            _context = context;
        }
        public async Task<RelatorioDto> RelatorioAulas(int alunoId)
        {
            var hoje = DateTime.UtcNow;
            var aluno = await _context.Alunos.FindAsync(alunoId);
            var primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(1);
            var aulasAgendadasMes = await _context.Agendamento
                .CountAsync(a =>
                a.AlunoId == alunoId &&
                a.Aula.DataHora >= primeiroDiaDoMes &&
                a.Aula.DataHora < primeiroDiaDoProximoMes);

            var listaAulasMaisFrequentes = await _context.Agendamento.Where(ag => ag.AlunoId == alunoId &&
              ag.Aula.DataHora >= primeiroDiaDoMes &&
              ag.Aula.DataHora < primeiroDiaDoProximoMes)
                .GroupBy(a => a.Aula.TipoAula)
                .Select(g => new FrequenciaAulasDto
                {
                    TipoAula = g.Key,
                    Quantidade = g.Count()
                }).OrderByDescending(i => i.Quantidade).ToListAsync();

            var relatorio = new RelatorioDto
            {
                TotalAulaMes = aulasAgendadasMes,
                AulaMaisFrequente = listaAulasMaisFrequentes
            };

            return relatorio;
        }
    }
}
