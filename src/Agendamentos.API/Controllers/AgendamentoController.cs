using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AgendamentoController : Controller
{
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentoController(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;

    }

    [HttpPost(Name = "Agendamento")]
    public async Task<IActionResult> AddAgendamento(int alunoId, int aulaId)
    {
        try
        {
            await _agendamentoService.Cadastrar(alunoId, aulaId);
            return Ok("Agendamento realizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }

    }

    [HttpDelete("{id}", Name = "ApagarAgendamento")]
    public async Task<IActionResult> ApagarAgendamento(int id)
    {
        try
        {
            await _agendamentoService.Apagar(id);
            return Ok("Agendamento apagado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpPut("{id}", Name = "AtualizarAgendamento")]
    public async Task<IActionResult> AtualizarAgendamento(int id, [FromBody] AgendamentoDto agendamentoDto)
    {
        try
        {
            await _agendamentoService.Editar(id, agendamentoDto);
            return Ok("Agendamento atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpGet(Name = "ListarAgendamentos")]
    public async Task<IActionResult> ListarAgendamentos()
    {
        try
        {
            var agendamentos = await _agendamentoService.ListarTodos();
            return Ok(agendamentos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "ListarAgendamentoPorId")]
    public async Task<IActionResult> ListarAgendamentoPorId(int id)
    {
        try
        {
            var agendamento = await _agendamentoService.ListarPorId(id);
            return Ok(agendamento);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro: {ex.Message}");
        }
    }
}
