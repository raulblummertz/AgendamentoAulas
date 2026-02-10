using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Agendamento.Infrastructure.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly AgendamentoContext _context;

    public AgendamentoRepository(AgendamentoContext context)
    {
        _context = context;
    }

    public async Task<Agendamentos?> GetByIdAsync(int id)
    {
        return await _context.Agendamentos.FindAsync(id);
    }

    public async Task<IEnumerable<Agendamentos>> GetAllAsync()
    {
        return await _context.Agendamentos.ToListAsync();
    }

    public async Task AddAsync(Agendamentos agendamento)
    {
        await _context.Agendamentos.AddAsync(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Agendamentos agendamento)
    {
        _context.Agendamentos.Update(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var agendamento = await GetByIdAsync(id);
        if (agendamento != null)
        {
            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> CountAgendamentosMesAsync(int alunoId, int aulaId)
    {
        return await _context.Agendamentos
            .Include(a => a.Aula)
            .CountAsync(a =>
                a.AlunoId == alunoId &&
                a.AulaId == aulaId &&
                a.Aula.DataHora.Month == DateTime.Now.Month &&
                a.Aula.DataHora.Year == DateTime.Now.Year);
    }

    public async Task<int> CountParticipantesAula(int aulaId)
    {
        return await _context.Agendamentos
            .CountAsync(a => a.AulaId == aulaId);
    }

    public async Task<int> CountAulasMesAsync(
       int alunoId,
       DateTime dataInicio,
       DateTime dataFim)
    {
        return await _context.Agendamentos
            .Include(a => a.Aula)
            .CountAsync(a =>
                a.AlunoId == alunoId &&
                a.Aula.DataHora >= dataInicio &&
                a.Aula.DataHora < dataFim);
    }

    public async Task<List<(string TipoAula, int Quantidade)>> GetAulasMaisFrequentesAsync(
        int alunoId,
        DateTime dataInicio,
        DateTime dataFim)
    {
        var resultado = await _context.Agendamentos
            .Include(a => a.Aula)
            .Where(ag =>
                ag.AlunoId == alunoId &&
                ag.Aula.DataHora >= dataInicio &&
                ag.Aula.DataHora < dataFim)
            .GroupBy(a => a.Aula.TipoAula)
            .Select(g => new
            {
                TipoAula = g.Key,
                Quantidade = g.Count()
            }
            ).ToListAsync();
        
        return resultado.OrderByDescending(r => r.Quantidade)
            .Select(r => (r.TipoAula, r.Quantidade))
            .ToList();
    }


}
