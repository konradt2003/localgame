using LocalGames.Models.Categoria;

namespace LocalGames.Domain.Dtos.Response;

public class ObterTodosJogosResponse
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public CategoriaJogo Categoria { get; set; }
    public bool Disponivel { get; set; }
    public string Responsavel { get; set; }
    public DateTime? DataDevolucaoLimite { get; set; }
    public bool EmAtraso { get; set; }
}
