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

}
