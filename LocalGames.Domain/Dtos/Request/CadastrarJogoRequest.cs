using LocalGames.Domain.Dtos.Request;
namespace LocalGames.Domain.Dtos.Request
{
    public class CadastrarJogoRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }  // categoria como string
    }
}
