using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AulasController : Controller
{
    private readonly IServiceBase<AulaDto> _aulaService;
    public AulasController(IServiceBase<AulaDto> aulaService)
    {
        _aulaService = aulaService;
    }

    [HttpPost(Name = "CadastroAula")]
    public async Task<IActionResult> CadastroAula([FromBody] AulaDto aulaDto)
    {
        try
        {
            await _aulaService.Cadastrar(aulaDto);
            return Ok("Aula cadastrada com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpGet(Name = "ListarAulas")]
    public async Task<IActionResult> ListarAulas()
    {
        try
        {
            var aulas = await _aulaService.ListarTodos();
            return Ok(aulas);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "ListarAulaPorId")]
    public async Task<IActionResult> ListarAulaPorId(int id)
    {
        try
        {
            var aula = await _aulaService.ListarPorId(id);
            return Ok(aula);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpPut("{id}", Name = "EditarAula")]
    public async Task<IActionResult> EditarAula(int id, [FromBody] AulaDto aulaDto)
    {
        try
        {
            await _aulaService.Editar(id, aulaDto);
            return Ok("Aula editada com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpDelete("{id}", Name = "ApagarAula")]
    public async Task<IActionResult> ApagarAula(int id)
    {
        try
        {
            await _aulaService.Apagar(id);
            return Ok("Aula apagada com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }
}

