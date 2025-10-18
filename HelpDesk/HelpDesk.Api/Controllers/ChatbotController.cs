using HelpDesk.Api.Services;
using HelpDesk.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ChatbotController : ControllerBase
{
    private readonly IChatbotService _chatbotService;

    public ChatbotController(IChatbotService chatbotService)
    {
        _chatbotService = chatbotService;
    }

    [HttpPost("processar")]
    public async Task<IActionResult> ProcessarMensagem([FromBody] ChatbotRequestDto request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Mensagem))
        {
            return BadRequest("Mensagem não pode ser nula ou vazia.");
        }

        if (request.ClienteId <= 0)
        {
            return BadRequest("ClienteId inválido.");
        }

        var resposta = await _chatbotService.ProcessarMensagemAsync(request);

        if (resposta.Tipo == HelpDesk.Shared.Enums.TipoRespostaChatbot.Erro)
        {
            return StatusCode(500, resposta);
        }

        return Ok(resposta);
    }
}