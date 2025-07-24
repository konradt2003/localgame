using LocalGames.Domain.Dtos.Response;
using LocalGames.Models.Jogo;


namespace LocalGames.Domain.Dtos.Services;

public interface IJogoService
{
    Task<long> CadastrarJogo(Jogo jogo);
    Task AtualizarJogo(long id, Jogo jogo);
    Task<List<ObterTodosJogosResponse>> ObterTodos();
    Task<ObterTodosJogosResponse> ObterDetalhado(long id);
    Task AlugarJogo(Jogo jogo);
    Task RetornarJogo(long id);
    Task ApagarJogo(long id);
}
