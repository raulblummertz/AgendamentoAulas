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
        await _agendamentoService.AddAgendamento(alunoId, aulaId);
        return Ok("Agendamento realizado com sucesso!");
    }

    [HttpDelete("{id}", Name = "ApagarAgendamento")]
    public async Task<IActionResult> ApagarAgendamento(int id)
    {
        await _agendamentoService.ApagarAgendamento(id);
        return Ok("Agendamento apagado com sucesso!");

    }

    [HttpPut("{id}", Name = "AtualizarAgendamento")]
    public async Task<IActionResult> AtualizarAgendamento(AgendamentoDto agendamentoDto)
    {
        await _agendamentoService.AtualizarAgendamento(agendamentoDto);
        return Ok("Agendamento atualizado com sucesso!");
    }

    [HttpGet(Name = "ListarAgendamentos")]
    public async Task<IActionResult> ListarAgendamentos()
    {
        var agendamentos = await _agendamentoService.ListarAgendamentos();
        return Ok(agendamentos);
    }

    [HttpGet("{id}", Name = "ListarAgendamentoPorId")]
    public async Task<IActionResult> ListarAgendamentoPorId(int id)
    {
        var agendamento = await _agendamentoService.ListarAgendamentosPorId(id);
        return Ok(agendamento);
    }
}
