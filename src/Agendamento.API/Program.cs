
using Microsoft.EntityFrameworkCore;
using Agendamento.Domain.Interfaces;
using Agendamento.Application.Services;
using Agendamento.Application.Interfaces;
using Agendamento.Infrastructure.Data;
using Agendamento.Infrastructure.Repositories;
using Agendamento.API.Validators;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Factories;
using FluentValidation;



namespace Agendamento.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString n�o configurada");

        var password = builder.Configuration["Supabase:Password"] ?? throw new InvalidOperationException("SupabasePassword n�o configurada"); 

        connectionString = connectionString.Replace("Password=;", $"Password={password};");

        // DbContext
        builder.Services.AddDbContext<AgendamentoContext>(options => options.UseNpgsql(connectionString));

        // Repositories
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<IAulaRepository, AulaRepository>();
        builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        builder.Services.AddScoped<IAgendamentoQueryRepository, AgendamentoQueryRepository>();

        // Services
        builder.Services.AddScoped<IAlunoService, AlunoService>();
        builder.Services.AddScoped<IAulaService, AulaService>();
        builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
        builder.Services.AddScoped<RelatorioService, RelatorioService>();
        
        // Factory
        builder.Services.AddSingleton<PlanoStrategyFactory>();

        builder.Services.AddControllers();
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
      
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var factory = scope.ServiceProvider.GetRequiredService<PlanoStrategyFactory>();
            Aluno.ConfigurarFactory(factory);
        }

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

