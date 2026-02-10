using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Agendamento.Infrastructure.Repositories;

public class AulaRepository : IAulaRepository
{
    private readonly AgendamentoContext _context;

    public AulaRepository(AgendamentoContext context)
    {
        _context = context;
    }

    public async Task<Aula?> GetByIdAsync(int id)
    {
        return await _context.Aulas.FindAsync(id);
        
    }

    public async Task<IEnumerable<Aula>> GetAllAsync()
    {
        return await _context.Aulas.ToListAsync();
       
    }

    public async Task AddAsync(Aula aula)
    {
        await _context.Aulas.AddAsync(aula);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aula aula)
    {
        _context.Aulas.Update(aula);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var aula = await GetByIdAsync(id);
        if (aula != null)
        {
            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
        }
    }
}
