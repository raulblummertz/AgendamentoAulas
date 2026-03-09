using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AlunosController : Controller
{
    private readonly IServiceBase<AlunoDto> _alunoService;
    public AlunosController(IServiceBase<AlunoDto> alunoService)
    {
        _alunoService = alunoService;
    }
    [HttpPost(Name = "CadastroAluno")]
    public async Task<IActionResult> CadastroAluno([FromBody] AlunoDto alunoDto)
    {
        try
        {
            await _alunoService.Cadastrar(alunoDto);
            return Ok("Aluno cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpPut("{id}", Name = "EditarAluno")]
    public async Task<IActionResult> EditarAluno(int id, [FromBody] AlunoDto alunoDto)
    {
        try
        {
            await _alunoService.Editar(id, alunoDto);
            return Ok("Aluno editado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
        
    }

    [HttpGet(Name = "ListarAlunos")]
    public async Task<IActionResult> ListarAlunos()
    {
        try
        {
            var alunos = await _alunoService.ListarTodos();
            return Ok(alunos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "ListarAluno")]
    public async Task<IActionResult> ListarAluno(int id)
    {
        try
        {
            var aluno = await _alunoService.ListarPorId(id);
            return Ok(aluno);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpDelete("{id}", Name = "ApagarAluno")]
    public async Task<IActionResult> ApagarAluno(int id)
    {
        try
        {
            await _alunoService.Apagar(id);
            return Ok("Aluno apagado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }
}
