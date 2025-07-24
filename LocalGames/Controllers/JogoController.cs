using Microsoft.AspNetCore.Mvc;
using LocalGames.Domain.Dtos.Request;
using LocalGames.Models.Jogo;
using LocalGames.Models.Categoria;

using LocalGames.Domain.Dtos.Services;

namespace LocalGames.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogoController(IJogoService jogoService) 
        {
            _jogoService = jogoService;
        }


        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var jogos = await _jogoService.ObterTodos();
            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterDetalhado(long id)
        {
            var jogo = await _jogoService.ObterDetalhado(id);
            if (jogo == null)
                return NotFound("Jogo não encontrado.");
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarJogoRequest request)
        {
            if (!Enum.TryParse<CategoriaJogo>(request.Categoria, true, out var categoria))
                return BadRequest("Categoria inválida.");

            var jogo = new Jogo
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Categoria = categoria,
                Disponivel = true,
                Responsavel = null,
                DataDevolucaoLimite = null
            };

            var id = await _jogoService.CadastrarJogo(jogo);

            return CreatedAtAction(nameof(ObterDetalhado), new { id = id }, jogo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, [FromBody] CadastrarJogoRequest request)
        {
            if (!Enum.TryParse<CategoriaJogo>(request.Categoria, true, out var categoria))
                return BadRequest("Categoria inválida.");

            var jogo = new Jogo
            {
                Id = id,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Categoria = categoria,
                Disponivel = true,
                Responsavel = null,
                DataDevolucaoLimite = null
            };

            await _jogoService.AtualizarJogo(id, jogo);

            return NoContent();
        }

        [HttpPost("alugar")]
        public async Task<IActionResult> Alugar([FromBody] Jogo jogo)
        {
            await _jogoService.AlugarJogo(jogo);
            return Ok("Jogo alugado com sucesso.");
        }

        [HttpPost("retornar/{id}")]
        public async Task<IActionResult> Retornar(long id)
        {
            await _jogoService.RetornarJogo(id);
            return Ok("Jogo devolvido com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Apagar(long id)
        {
            await _jogoService.ApagarJogo(id);
            return Ok("Jogo removido.");
        }
    }
}
