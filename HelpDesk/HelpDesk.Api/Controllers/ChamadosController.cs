// Local: HelpDesk.Api/Controllers/ChamadosController.cs

using HelpDesk.Api.Data.Repositories;
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
					ClienteId = c.ClienteId
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
			// Este método será implementado em breve, mas é necessário para o CreatedAtAction funcionar
			var chamado = await _chamadoService.GetChamadoByIdAsync(chamadoId);
			if (chamado == null) return NotFound();

			// Aqui também faremos o mapeamento para DTO
			return Ok(chamado); // Por enquanto, retornamos o modelo
		}
	}
}