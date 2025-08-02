using Agendamento01.Aulas.DTO;
using Agendamento01.Aulas.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agendamento01.Controllers
{
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
        [HttpPost(Name = "AgendamentoAula")]
        public async Task<IActionResult> AgendamentoAula(int alunoId, int aulaId)
        {
            await _aulaService.Agendamento(alunoId, aulaId);
            return Ok("Agendamento concluído com sucesso!");
        }

    }
}
