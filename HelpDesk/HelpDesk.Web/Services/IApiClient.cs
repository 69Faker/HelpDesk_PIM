// Local: HelpDesk.Web/Services/IApiClient.cs
using HelpDesk.Shared.Dtos;
using System.Collections.Generic; // <-- Adicione este 'using'

namespace HelpDesk.Web.Services
{
    public interface IApiClient
    {
        Task<ClienteLoginResponseDto?> LoginAsync(ClienteLoginRequestDto loginRequest);
        Task<bool> CriarChamadoAsync(ChamadoCreateRequestDto chamadoRequest);

        // --- MÉTODO NOVO ADICIONADO ---
        // (Estava comentado "Futuramente" no seu arquivo)
        Task<List<ChamadoDto>> GetChamadosPorClienteAsync(int clienteId);
        Task<ChatbotResponseDto> ProcessarMensagemAsync(ChatbotRequestDto request);
    }
}