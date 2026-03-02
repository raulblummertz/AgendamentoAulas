namespace Agendamento.Application.Interfaces;

public interface IServiceBase<TDto> where TDto : class
{
    Task<TDto> ListarPorId(int id);
    Task<IEnumerable<TDto>> ListarTodos();
    Task Cadastrar(TDto dto);
    Task Editar(int id, TDto dto);
    Task Apagar(int id);
}