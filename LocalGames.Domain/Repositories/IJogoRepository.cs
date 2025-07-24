
using LocalGames.Models.Jogo;

namespace LocalGames.Repositories;

public interface IJogoRepository
{
    Task<IEnumerable<Jogo>>ObterTodos();
    Task<Jogo>ObterTodosDetalhado(long id);
    Task<long> CadastrarJogo(Jogo jogo);
    Task AtualizarJogo(long id, Jogo jogo);
    Task AlugarJogo(Jogo jogo);
    Task RetornarJogo(long id);   
    Task ApagarJogo(long id);   
}
    