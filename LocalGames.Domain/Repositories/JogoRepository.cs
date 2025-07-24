using LocalGames.Models.Jogo;

namespace LocalGames.Repositories;

public class JogoRepository : IJogoRepository
{
    private static readonly List<Jogo> _jogos = new();
    private static long _proximoId = 1;

    public Task<long> CadastrarJogo(Jogo jogo)
    {
        jogo.Id = _proximoId++;
        _jogos.Add(jogo);
        return Task.FromResult(jogo.Id);
    }

    public Task AtualizarJogo(long id, Jogo jogoAtualizado)
    {
        var index = _jogos.FindIndex(j => j.Id == id);
        if (index == -1) return Task.CompletedTask;

        var existente = _jogos[index];

        existente.Titulo = jogoAtualizado.Titulo;
        existente.Descricao = jogoAtualizado.Descricao;
        existente.Categoria = jogoAtualizado.Categoria;
        existente.Disponivel = jogoAtualizado.Disponivel;
        existente.Responsavel = jogoAtualizado.Responsavel;
        existente.DataDevolucaoLimite = jogoAtualizado.DataDevolucaoLimite;

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Jogo>> ObterTodos()
    {
        return Task.FromResult(_jogos.AsEnumerable());
    }

    public Task<Jogo> ObterTodosDetalhado(long id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);
        return Task.FromResult(jogo);
    }

    public Task AlugarJogo(Jogo jogoAlugado)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == jogoAlugado.Id);
        if (jogo == null) return Task.CompletedTask;

        jogo.Disponivel = false;
        jogo.Responsavel = jogoAlugado.Responsavel;
        jogo.DataDevolucaoLimite = DateTime.Now.AddDays(7); 

        return Task.CompletedTask;
    }

    public Task RetornarJogo(long id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);
        if (jogo == null) return Task.CompletedTask;

        jogo.Disponivel = true;
        jogo.Responsavel = null;
        jogo.DataDevolucaoLimite = null;

        return Task.CompletedTask;
    }

    public Task ApagarJogo(long id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);
        if (jogo != null)
        {
            _jogos.Remove(jogo);
        }

        return Task.CompletedTask;
    }
}
