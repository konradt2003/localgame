using LocalGames.Domain.Dtos.Response;
using LocalGames.Models.Jogo;
using LocalGames.Repositories;

namespace LocalGames.Domain.Dtos.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _repository;

    public JogoService()
    {
        _repository = new JogoRepository();
    }

    public async Task<long> CadastrarJogo(Jogo jogo)
    {
        jogo.Disponivel = true;
        jogo.Responsavel = null;
        jogo.DataDevolucaoLimite = null;

        return await _repository.CadastrarJogo(jogo);
    }

    public async Task AtualizarJogo(long id, Jogo jogo)
    {
        await _repository.AtualizarJogo(id, jogo);
    }

    public async Task<List<ObterTodosJogosResponse>> ObterTodos()
    {
        var jogos = await _repository.ObterTodos();

        return jogos.Select(j => new ObterTodosJogosResponse
        {
            Id = j.Id,
            Titulo = j.Titulo,
            Descricao = j.Descricao,
            Categoria = j.Categoria,
            Disponivel = j.Disponivel,
            Responsavel = j.Responsavel,
            DataDevolucaoLimite = j.DataDevolucaoLimite,
            EmAtraso = j.EmAtraso
        }).ToList();
    }

    public async Task<ObterTodosJogosResponse> ObterDetalhado(long id)
    {
        var jogo = await _repository.ObterTodosDetalhado(id);

        if (jogo == null)
            return null;

        return new ObterTodosJogosResponse
        {
            Id = jogo.Id,
            Titulo = jogo.Titulo,
            Descricao = jogo.Descricao,
            Categoria = jogo.Categoria,
            Disponivel = jogo.Disponivel,
            Responsavel = jogo.Responsavel,
            DataDevolucaoLimite = jogo.DataDevolucaoLimite,
            EmAtraso = jogo.EmAtraso
        };
    }

    public async Task AlugarJogo(Jogo jogo)
    {
        var jogoBanco = await _repository.ObterTodosDetalhado(jogo.Id);

        if (jogoBanco == null)
            throw new Exception("Jogo não encontrado.");

        if (!jogoBanco.Disponivel)
            throw new Exception("Jogo já está alugado.");

        jogoBanco.Disponivel = false;
        jogoBanco.Responsavel = jogo.Responsavel;
        jogoBanco.DataDevolucaoLimite = DateTime.Now.AddDays(7);

        await _repository.AtualizarJogo(jogoBanco.Id, jogoBanco);
    }

    public async Task RetornarJogo(long id)
    {
        var jogo = await _repository.ObterTodosDetalhado(id);

        if (jogo == null)
            throw new Exception("Jogo não encontrado.");

        if (jogo.Disponivel)
            throw new Exception("Jogo já foi devolvido.");

        jogo.Disponivel = true;
        jogo.Responsavel = null;
        jogo.DataDevolucaoLimite = null;

        await _repository.AtualizarJogo(jogo.Id, jogo);
    }

    public async Task ApagarJogo(long id)
    {
        await _repository.ApagarJogo(id);
    }
}
