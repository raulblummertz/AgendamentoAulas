
using Microsoft.EntityFrameworkCore;
using Agendamento01.Data;
using Agendamento01.Alunos.Interfaces;
using Agendamento01.Alunos.Services;
using Agendamento01.Aulas.Interfaces;
using Agendamento01.Aulas.Services;
using Agendamento01.Relatorios.Services;



namespace Agendamento01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AgendamentoContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAlunoService, AlunoService>();
            builder.Services.AddScoped<IAulaService, AulaService>();
            builder.Services.AddScoped<RelatorioService, RelatorioService>();
            builder.Services.AddControllers();
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
}
