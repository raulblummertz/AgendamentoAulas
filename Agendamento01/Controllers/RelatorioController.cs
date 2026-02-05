using Agendamento01.Relatorios.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento01.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RelatorioController : Controller
    {
        private readonly RelatorioService _relatorioService;

        public RelatorioController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }
        [HttpGet(Name = "RelatorioAulas")]
        public async Task<IActionResult> AulasMaisFrequentes(int alunoId)
        {
            var result = await _relatorioService.RelatorioAulas(alunoId);
            return Ok(result);
        }
    }
}
