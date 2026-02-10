using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agendamento.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AulasController : Controller
{
    private readonly IAulaService _aulaService;
    public AulasController(IAulaService aulaService)
    {
        _aulaService = aulaService;
    }

    [HttpPost(Name = "CadastroAula")]
    public async Task<IActionResult> CadastroAula(AulaDto aulaDto)
    {
        await _aulaService.CadastroAula(aulaDto);
        return Ok("Aula cadastrada com sucesso!");
    }

    [HttpGet(Name = "ListarAulas")]
    public async Task<IActionResult> ListarAulas()
    {
        var aulas = await _aulaService.ListarAulas();
        return Ok(aulas);
    }

    [HttpGet("{id}", Name = "ListarAulaPorId")]
    public async Task<IActionResult> ListarAulaPorId(int id)
    {
        var aula = await _aulaService.ListarAulaPorId(id);
        return Ok(aula);
    }

    [HttpPut("{id}", Name = "EditarAula")]
    public async Task<IActionResult> EditarAula(int id, AulaDto aulaDto)
    {
        await _aulaService.EditarAula(aulaDto);
        return Ok("Aula editada com sucesso!");
    }

    [HttpDelete("{id}", Name = "ApagarAula")]
    public async Task<IActionResult> ApagarAula(int id)
    {
        await _aulaService.ApagarAula(id);
        return Ok("Aula apagada com sucesso!");

    }
}

