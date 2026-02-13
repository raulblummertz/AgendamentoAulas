using Agendamento.Application.DTOs;
using Agendamento.Application.Services;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Enums;
using Moq;
using Xunit;

namespace Agendamento.Tests.Services;

public class AlunoServiceTests
{
    [Fact]
    public async Task ListarAlunoPorId_DeveRetornarAluno_QuandoIdExiste()
    {
        
        var mockRepository = new Mock<IAlunoRepository>();
        
        var alunoId = 1;
        var alunoEsperado = new Aluno("João Silva", EnumTipoPlano.Mensal);
        
        typeof(Aluno).GetProperty("Id")?.SetValue(alunoEsperado, alunoId);
        
        mockRepository.Setup(repo => repo.GetByIdAsync(alunoId)).ReturnsAsync(alunoEsperado);
        
        var alunoService = new AlunoService(mockRepository.Object);

        var resultado = await alunoService.ListarAlunoPorId(alunoId);

        Assert.NotNull(resultado);
        Assert.Equal(alunoId, resultado.Id);
        Assert.Equal("João Silva", resultado.Nome);
        Assert.Equal(EnumTipoPlano.Mensal, resultado.Plano);
        
        mockRepository.Verify(repo => repo.GetByIdAsync(alunoId),Times.Once);
    }

    [Fact]
    public async Task EditarAluno_DeveRetornarAlunoEditado()
    {
        var mockRepository = new Mock<IAlunoRepository>();
        
        var alunoId = 1;
        var alunoEsperado = new Aluno("Mariazinha", EnumTipoPlano.Trimestral);
        var alunoAtualizado = new Aluno("Maria", EnumTipoPlano.Anual);
        var alunoEditado = new AlunoDto
        {
            Nome = "Maria", 
            Plano = EnumTipoPlano.Anual
        };
        typeof(Aluno).GetProperty("Id")?.SetValue(alunoEsperado, alunoId);
        typeof(Aluno).GetProperty("Id")?.SetValue(alunoAtualizado, alunoId);
        
        mockRepository.Setup(repo => repo.GetByIdAsync(alunoId)).ReturnsAsync(alunoEsperado);
        mockRepository.SetupSequence(repo => repo.GetByIdAsync(alunoId)).ReturnsAsync(alunoEsperado).ReturnsAsync(alunoAtualizado);
        mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Aluno>())).Returns(Task.CompletedTask);
        
        var alunoService = new AlunoService(mockRepository.Object);

        await alunoService.EditarAluno(alunoId, alunoEditado);
        
        var resultado = await alunoService.ListarAlunoPorId(alunoId);
        
        Assert.NotNull(resultado);
        Assert.Equal(alunoId, resultado.Id);
        Assert.Equal(alunoEditado.Nome, resultado.Nome);
        Assert.Equal(alunoEditado.Plano, resultado.Plano);
        
        mockRepository.Verify(repo => repo.GetByIdAsync(alunoId),Times.Exactly(2));
        mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Aluno>(a => a.Nome == alunoEditado.Nome && a.Plano == alunoEditado.Plano)),Times.Once);
    }
}
