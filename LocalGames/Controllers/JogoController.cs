using Microsoft.AspNetCore.Mvc;
using LocalGames.DTOs;
using LocalGames.Model;
using LocalGames.Repositories;
using System;
using System.Linq;

namespace LocalGames.Controllers
{
    [ApiController]
    [Route("jogo")]
    public class JogoController : ControllerBase
    {
        private static IJogoRepository _repository = new JogoRepository();

        [HttpPost]
        public IActionResult CriarJogo([FromBody] JogoDTO dto)
        {
            // Validação simples
            if (string.IsNullOrEmpty(dto.Titulo) || string.IsNullOrEmpty(dto.Descricao) || string.IsNullOrEmpty(dto.Categoria))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            Categoria categoria;
            if (!Enum.TryParse(dto.Categoria, true, out categoria))
            {
                return BadRequest("Categoria inválida. Use: BRONZE, PRATA ou OURO.");
            }

            var novoJogo = new Jogo
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Categoria = categoria,
                Disponivel = true,
                Responsavel = null,
                DataRetirada = null
            };

            _repository.Adicionar(novoJogo);

            return Created("", novoJogo);
        }

        [HttpGet]
        public IActionResult ListarJogos()
        {
            var jogos = _repository.ListarTodos();

            var resposta = jogos.Select(j => new JogoRespostaDTO
            {
                Id = j.Id,
                Titulo = j.Titulo,
                Disponivel = j.Disponivel,
                Categoria = j.Categoria.ToString(),
                DataRetirada = j.DataRetirada,
                IsEmAtraso = j.DataRetirada.HasValue && j.DataRetirada.Value.Date < DateTime.Now.Date
            }).ToList();

            return Ok(resposta);
        }

        [HttpPut("{id}/alugar")]
        public IActionResult AlugarJogo(long id, [FromQuery] string responsavel)
        {
            var jogo = _repository.ObterPorId(id);
            if (jogo == null)
                return NotFound("Jogo não encontrado.");

            if (!jogo.Disponivel)
                return BadRequest("Jogo não está disponível para locação.");

            jogo.Disponivel = false;
            jogo.Responsavel = responsavel ?? "Desconhecido";

         
            switch (jogo.Categoria)
            {
                case Categoria.BRONZE:
                    jogo.DataRetirada = DateTime.Now.AddDays(9);
                    break;
                case Categoria.PRATA:
                    jogo.DataRetirada = DateTime.Now.AddDays(6);
                    break;
                case Categoria.OURO:
                    jogo.DataRetirada = DateTime.Now.AddDays(3);
                    break;
                default:
                    return BadRequest("Categoria inválida.");
            }

            _repository.Atualizar(jogo);

            return Ok("Jogo alugado com sucesso!");
        }

    }
}
