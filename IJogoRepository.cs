using LocalGames.Model;
using System.Collections.Generic;

namespace LocalGames.Repositories
{
    public interface IJogoRepository
    {
        void Adicionar(Jogo jogo);
        List<Jogo> ListarTodos();
        Jogo? ObterPorId(long id);
        void Atualizar(Jogo jogo);
    }
}
