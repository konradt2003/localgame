using LocalGames.Models.Categoria;
using System;

namespace LocalGames.Models.Jogo
{
    public class Jogo
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public CategoriaJogo Categoria { get; set; }
        public bool Disponivel { get; set; }
        public string? Responsavel { get; set; }
        public DateTime? DataDevolucaoLimite { get; set; }

        public bool EmAtraso
        {
            get
            {
                if (Disponivel || DataDevolucaoLimite == null)
                    return false;

                return DateTime.Now.Date > DataDevolucaoLimite.Value.Date;
            }
        }
    }
}
