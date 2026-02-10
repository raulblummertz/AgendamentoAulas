using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infrastructure.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly AgendamentoContext _context;

    public AlunoRepository(AgendamentoContext context)
    {
        _context = context;
    }

    public async Task<Aluno?> GetByIdAsync(int id)
    {
        return await _context.Alunos.FindAsync(id);
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return await _context.Alunos.ToListAsync();
    }

    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var aluno = await GetByIdAsync(id);
        if (aluno != null)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
