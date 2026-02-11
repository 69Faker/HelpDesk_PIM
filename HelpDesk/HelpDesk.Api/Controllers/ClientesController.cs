// Local: HelpDesk.Api/Controllers/ClientesController.cs

using HelpDesk.Api.Services;
using HelpDesk.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [ApiController] // Define que esta classe é um Controller de API
    [Route("api/[controller]")] // Define a rota base para este controller: "api/clientes"
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        // O sistema de injeção de dependência vai nos fornecer o ClienteService
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // POST: api/clientes/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ClienteLoginRequestDto requestDto)
        {
            try
            {
                // 1. Chama o serviço de negócio para autenticar o cliente
                var cliente = await _clienteService.AutenticarAsync(requestDto.Cpf, requestDto.Senha);

                // 2. Se o serviço retornar nulo, o login falhou (CPF ou senha errados)
                if (cliente == null)
                {
                    // Retorna um status 404 Not Found com uma mensagem segura
                    return NotFound(new { message = "CPF ou senha inválidos." });
                }

                // 3. Se o login for bem-sucedido, nós mapeamos o resultado para um DTO de resposta
                // Isso garante que NUNCA vamos retornar dados sensíveis, como a SenhaHash.
                var responseDto = new ClienteLoginResponseDto
                {
                    IdCliente = cliente.IdCliente,
                    Nome = cliente.Nome,
                    Email = cliente.Email
                };

                // 4. Retorna um status 200 OK com os dados do cliente no DTO de resposta
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                // Em caso de um erro inesperado no servidor, retorna um status 500
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
        // PUT: api/clientes/1/alterar-senha
        [HttpPut("{id}/alterar-senha")]
        public async Task<IActionResult> AlterarSenha(int id, [FromBody] ClienteAlterarSenhaRequestDto requestDto)
        {
            try
            {
                // Chama o serviço que já tem a lógica de negócio pronta
                await _clienteService.AlterarSenhaAsync(id, requestDto.SenhaAntiga, requestDto.NovaSenha);

                // Se o serviço executou sem erros, a senha foi alterada.
                // Retornamos 204 No Content, que significa "Sucesso, nada a devolver".
                return NoContent();
            }
            catch (Exception ex)
            {
                // Se o serviço lançou uma exceção (ex: "Senha antiga incorreta"),
                // retornamos um 400 Bad Request com a mensagem de erro.
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("redefinir-senha")]
        public async Task<IActionResult> RedefinirSenha([FromBody] ClienteRedefinirSenhaRequestDto requestDto)
        {
            try
            {
                // 1. Chama o serviço com a nova lógica
                await _clienteService.RedefinirSenhaAsync(requestDto.Cpf, requestDto.Email, requestDto.NovaSenha);

                // 2. Retorna sucesso
                return Ok(new { message = "Senha redefinida com sucesso." });
            }
            catch (Exception ex)
            {
                // 3. Pega erros (ex: "CPF ou E-mail inválidos.") e os retorna como BadRequest
                return BadRequest(new { message = ex.Message });
            }
        }

        // Aqui, no futuro, podemos adicionar outros endpoints como:
        // - POST /api/clientes (para registrar um novo cliente)
        // - PUT /api/clientes/{id}/alterarsenha (para alterar a senha)
    }
}