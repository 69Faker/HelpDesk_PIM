// Local: Services/IApiClient.cs
using HelpDesk.Shared.Dtos;

namespace HelpDesk.Web.Services
{
    public interface IApiClient
    {
        Task<ClienteLoginResponseDto?> LoginAsync(ClienteLoginRequestDto loginRequest);
        Task<bool> CriarChamadoAsync(ChamadoCreateRequestDto chamadoRequest);
        // Futuramente: Task<List<ChamadoDto>> GetChamadosAsync(int clienteId);
    }
}