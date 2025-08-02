using Agendamento01.Alunos.DTO;
using Agendamento01.Alunos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento01.Controllers
{
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
    }
}
