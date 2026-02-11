using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agendamento.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AulasController : Controller
{
    private readonly IAulaService _aulaService;
    private readonly IValidator<AulaDto> _aulaDtoValidator;
    public AulasController(IAulaService aulaService, IValidator<AulaDto> aulaDtoValidator)
    {
        _aulaService = aulaService;
        _aulaDtoValidator = aulaDtoValidator;
    }

    [HttpPost(Name = "CadastroAula")]
    public async Task<IActionResult> CadastroAula([FromBody] AulaDto aulaDto)
    {
        var result = await _aulaDtoValidator.ValidateAsync(aulaDto);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));
        }
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
    public async Task<IActionResult> EditarAula(int id, [FromBody] AulaDto aulaDto)
    {
        var result = await _aulaDtoValidator.ValidateAsync(aulaDto);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));
        }
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

