// Local: Services/ApiClient.cs
using HelpDesk.Shared.Dtos;
using System.Net.Http.Json;

namespace HelpDesk.Web.Services
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Injetamos a "Fábrica" de HttpClients
        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Método de Login
        public async Task<ClienteLoginResponseDto?> LoginAsync(ClienteLoginRequestDto loginRequest)
        {
            try
            {
                // Pegamos o cliente nomeado "HelpDeskApi" que você registrou no Program.cs
                var client = _httpClientFactory.CreateClient("HelpDeskApi");

                // Chama a rota "api/Clientes/login"
                var response = await client.PostAsJsonAsync("api/Clientes/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ClienteLoginResponseDto>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar fazer login: {ex.Message}");
                return null;
            }
        }

        // Método de Criar Chamado
        public async Task<bool> CriarChamadoAsync(ChamadoCreateRequestDto chamadoRequest)
        {
            try
            {
                // Pegamos o cliente nomeado "HelpDeskApi"
                var client = _httpClientFactory.CreateClient("HelpDeskApi");

                // Chama a rota "api/Chamados"
                var response = await client.PostAsJsonAsync("api/Chamados", chamadoRequest);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar chamado: {ex.Message}");
                return false;
            }
        }
    }
}