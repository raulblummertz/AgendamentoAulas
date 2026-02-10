using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AlunosController : Controller
{
    private readonly IAlunoService _alunoService;
    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }
    [HttpPost(Name = "CadastroAluno")]
    public async Task<IActionResult> CadastroAluno(AlunoDto alunoDto)
    {
        await _alunoService.CadastroAluno(alunoDto);
        return Ok("Aluno cadastrado com sucesso!");
    }

    [HttpPut("{id}", Name = "EditarAluno")]
    public async Task<IActionResult> EditarAluno(int id, AlunoDto alunoDto)
    {
        await _alunoService.EditarAluno(id, alunoDto);
        return Ok("Aluno editado com sucesso!");
    }

    [HttpGet(Name = "ListarAlunos")]
    public async Task<IActionResult> ListarAlunos()
    {
        var alunos = await _alunoService.ListarAlunos();
        return Ok(alunos);
    }

    [HttpGet("{id}", Name = "ListarAluno")]
        public async Task<IActionResult> ListarAluno(int id)
    {
        var aluno = await _alunoService.ListarAlunoPorId(id);
        return Ok(aluno);
    }

    [HttpDelete("{id}", Name = "ApagarAluno")]
    public async Task<IActionResult> ApagarAluno(int id)
    {
        await _alunoService.ApagarAluno(id);
        return Ok("Aluno apagado com sucesso!");
    }
}
