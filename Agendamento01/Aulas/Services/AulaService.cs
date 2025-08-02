using Agendamento01.Aulas.DTO;
using Agendamento01.Aulas.Interfaces;
using Agendamento01.Aulas.Model;
using Agendamento01.Data;
using Microsoft.EntityFrameworkCore;

namespace Agendamento01.Aulas.Services
{
    public class AulaService : IAulaService
    {
        private readonly AgendamentoContext _context;

        public AulaService(AgendamentoContext context)
        {
            _context = context;
        }
        public async Task Agendamento(int alunoId, int aulaId)
        {
            var aluno = await _context.Alunos.FindAsync(alunoId);
            var aula = await _context.Aulas.FindAsync(aulaId);
            var aulasAgendadasMes = await _context.Agendamento
                .CountAsync(a => 
                a.AlunoId == alunoId && 
                a.AulaId == aulaId &&
                a.Aula.DataHora.Month == DateTime.Now.Month &&
                a.Aula.DataHora.Year == DateTime.Now.Year);

            var participantesAtuais = await _context.Agendamento.CountAsync(a =>
            a.AulaId == aulaId);

            if (aluno == null) throw new KeyNotFoundException("Aluno não encontrado.");
            if (aula == null) throw new KeyNotFoundException("Aula não encontrada.");
            if (aluno.LimiteAulas <= aulasAgendadasMes)
                throw new InvalidOperationException($"O aluno atingiu o limite de {aluno.LimiteAulas} aulas mensais do plano {aluno.Plano}.");
            if (participantesAtuais >= aula.CapacidadeMaxima)
                throw new InvalidOperationException("Aula já está cheia.");
            if (aula.DataHora < DateTime.Now)
                throw new InvalidOperationException("Aula já ocorreu ou está agendada para o passado.");


            var novoAgendamento = new Agendamento(alunoId, aulaId);
            _context.Agendamento.Add(novoAgendamento);

            await _context.SaveChangesAsync();
        }

        public async Task CadastroAula(AulaDto aulaDto)
        {
            if (string.IsNullOrWhiteSpace(aulaDto.TipoAula))
                throw new ArgumentException("Tipo de aula não pode ser vazio.");

            var novaAula = new Aula(aulaDto.TipoAula, aulaDto.DataHora.ToUniversalTime(), aulaDto.CapacidadeMaxima);
            _context.Aulas.Add(novaAula);
            await _context.SaveChangesAsync();
        }
    }
}
