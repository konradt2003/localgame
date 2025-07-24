using LocalGames.Model;
using System.Collections.Generic;
using System.Linq;

namespace LocalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static List<Jogo> _jogos = new List<Jogo>();
        private static long _contadorId = 1;

        public void Adicionar(Jogo jogo)
        {
            jogo.Id = _contadorId++;
            _jogos.Add(jogo);
        }

        public List<Jogo> ListarTodos()
        {
            return _jogos;
        }

        public Jogo? ObterPorId(long id)
        {
            return _jogos.FirstOrDefault(j => j.Id == id);
        }

        public void Atualizar(Jogo jogo)
        {
            var index = _jogos.FindIndex(j => j.Id == jogo.Id);
            if (index >= 0)
            {
                _jogos[index] = jogo;
            }
        }
    }
}
