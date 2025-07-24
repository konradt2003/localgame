public class Jogo
{
    public long Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Categoria Categoria { get; set; }
    public bool Disponivel { get; set; }
    public string Responsavel { get; set; }
    public DateTime? DataDevolucaoLimite { get; set; } 
}
