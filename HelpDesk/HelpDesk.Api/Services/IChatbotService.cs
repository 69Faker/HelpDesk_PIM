using HelpDesk.Shared.Dtos;

namespace HelpDesk.Api.Services
{
    public interface IChatbotService
    {
        Task<ChatbotResponseDto> ProcessarMensagemAsync(ChatbotRequestDto request);
    }
}