public class JogoRespostaDTO
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public bool Disponivel { get; set; }
    public string Categoria { get; set; }
    public DateTime? DataDevolucaoLimite { get; set; } 
    public bool IsEmAtraso { get; set; }
}
