// Local: HelpDesk.Api/Controllers/ChamadosController.cs

using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Models;
using HelpDesk.Api.Services;
using HelpDesk.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HelpDesk.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")] // Rota base: "api/chamados"
	public class ChamadosController : ControllerBase
	{
		private readonly IChamadoService _chamadoService;

		public ChamadosController(IChamadoService chamadoService)
		{
			_chamadoService = chamadoService;
		}

		// GET: api/chamados/cliente/5
		[HttpGet("cliente/{clienteId}")]
		public async Task<IActionResult> GetPorCliente(int clienteId)
		{
			try
			{
				var chamados = await _chamadoService.GetChamadosPorClienteAsync(clienteId);

				// Mapeamento manual para DTOs de lista
				var chamadosDto = chamados.Select(c => new ChamadoDto
				{
					IdChamado = c.IdChamado,
					Titulo = c.Titulo,
					DataAbertura = c.DataAbertura,
					Status = c.Status,
					ClienteId = c.ClienteId,
                    Descricao = c.Descricao

                });

				return Ok(chamadosDto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Erro interno: {ex.Message}");
			}
		}

		// POST: api/chamados
		[HttpPost]
		public async Task<IActionResult> AbrirChamado([FromBody] ChamadoCreateRequestDto requestDto)
		{
			try
			{
				var novoChamado = await _chamadoService.AbrirChamadoAsync(
					requestDto.ClienteId,
                    requestDto.Categoria,
                    requestDto.Titulo,
					requestDto.Descricao);

				// Mapeamento manual para o DTO de detalhes para a resposta
				var responseDto = new ChamadoDetailDto
				{
					IdChamado = novoChamado.IdChamado,
					Titulo = novoChamado.Titulo,
					Descricao = novoChamado.Descricao,
					DataAbertura = novoChamado.DataAbertura,
					Status = novoChamado.Status,
					ClienteId = novoChamado.ClienteId
				};

				// Retorna 201 Created com a localização do novo recurso e o objeto criado
				return CreatedAtAction(nameof(GetChamadoById), new { chamadoId = responseDto.IdChamado }, responseDto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Erro interno: {ex.Message}");
			}
		}

        // GET: api/chamados/10
        [HttpGet("{chamadoId}")]
        public async Task<IActionResult> GetChamadoById(int chamadoId)
        {
            var chamado = await _chamadoService.GetChamadoByIdAsync(chamadoId);
            if (chamado == null) return NotFound();

            // --- ESTA É A CORREÇÃO ---
            // Nós mapeamos o Modelo (Chamado) para o DTO (ChamadoDetailDto).
            // Isso quebra o loop infinito, pois o DTO não tem a propriedade "Cliente".
            var dto = new ChamadoDetailDto
            {
                // Propriedades do ChamadoDto base
                IdChamado = chamado.IdChamado,
                Titulo = chamado.Titulo,
                DataAbertura = chamado.DataAbertura,
                Status = chamado.Status,
                ClienteId = chamado.ClienteId,

                // Propriedades do ChamadoDetailDto
                Categoria = chamado.Categoria,
                Descricao = chamado.Descricao,
                DataFechamento = chamado.DataFechamento,

                // Mapeia a lista de Modelos 'Mensagem' para uma lista de DTOs 'MensagemDto'
                Mensagens = chamado.Mensagens.Select(msg => new MensagemDto
                {
                    IdMensagem = msg.IdMensagem,
                    Texto = msg.Texto,
                    DataEnvio = msg.DataEnvio,
                    Autor = msg.Autor
                }).ToList()
            };

            return Ok(dto); // Retornamos o DTO limpo, em vez do modelo
        }
        // POST: api/chamados/5/mensagens
        [HttpPost("{chamadoId}/mensagens")]
        public async Task<IActionResult> AdicionarMensagem(int chamadoId, [FromBody] MensagemCreateRequestDto requestDto)
        {
            try
            {
                // Mapeamos o DTO para o nosso modelo de domínio
                var novaMensagem = new Mensagem
                {
                    Texto = requestDto.Texto,
                    Autor = requestDto.Autor
                    // A data de envio e o ChamadoId serão definidos pelo serviço
                };

                await _chamadoService.AdicionarMensagemAsync(chamadoId, novaMensagem);

                // Se a operação foi bem-sucedida, retornamos 204 No Content,
                // que é um status apropriado para "operação concluída, nada a devolver".
                // Ou poderíamos retornar 200 OK com o chamado atualizado.
                return NoContent();
            }
            catch (Exception ex)
            {
                // Se o serviço lançar uma exceção (ex: chamado não encontrado), retornamos um erro.
                // Um erro 404 seria mais específico se soubermos que foi "não encontrado".
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{chamadoId}/cancelar")]
        public async Task<IActionResult> CancelarChamado(int chamadoId)
        {
            try
            {
                await _chamadoService.CancelarChamadoAsync(chamadoId);
                return NoContent(); // Sucesso, sem conteúdo para retornar.
            }
            catch (Exception ex)
            {
                // Se o serviço lançar uma exceção, retornamos como BadRequest.
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}