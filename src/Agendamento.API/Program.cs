
using Microsoft.EntityFrameworkCore;
using Agendamento.Domain.Interfaces;
using Agendamento.Application.Services;
using Agendamento.Application.Interfaces;
using Agendamento.Infrastructure.Data;
using Agendamento.Infrastructure.Repositories;
using FluentValidation



namespace Agendamento.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString não configurada");

        var password = builder.Configuration["Supabase:Password"] ?? throw new InvalidOperationException("SupabasePassword não configurada"); 

        connectionString = connectionString.Replace("Password=;", $"Password={password};");

        // DbContext
        builder.Services.AddDbContext<AgendamentoContext>(options => options.UseNpgsql(connectionString));

        // Repositories
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<IAulaRepository, AulaRepository>();
        builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

        // Services
        builder.Services.AddScoped<IAlunoService, AlunoService>();
        builder.Services.AddScoped<IAulaService, AulaService>();
        builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
        builder.Services.AddScoped<RelatorioService, RelatorioService>();

        builder.Services.AddControllers().;
        builder.Services.AddValidatorsFromAssemblyContaining<AlunoDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AulaDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AgendamentoDtoValidator>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

