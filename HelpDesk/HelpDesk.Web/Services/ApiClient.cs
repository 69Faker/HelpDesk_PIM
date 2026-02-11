// Local: HelpDesk.Web/Services/ApiClient.cs

using HelpDesk.Shared.Dtos;
using System.Net.Http.Json;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
// --- ADICIONE ESTES 'USINGS' ---
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HelpDesk.Web.Services
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // --- ESTA É A CORREÇÃO PRINCIPAL ---
        // Criamos opções de JSON que sabem como converter Enums para Strings
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            // Converte Enums (ex: ABERTO) para Strings (ex: "ABERTO")
            Converters = { new JsonStringEnumConverter() },
            // Ajuda a ler o JSON mesmo que o 'case' seja diferente (ex: 'nome' vs 'Nome')
            PropertyNameCaseInsensitive = true
        };
        // --- FIM DA CORREÇÃO ---


        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // --- MÉTODO DE LOGIN (ATUALIZADO) ---
        public async Task<ClienteLoginResponseDto?> LoginAsync(ClienteLoginRequestDto loginRequest)
        {
            var client = _httpClientFactory.CreateClient("HelpDeskApi");
            var response = await client.PostAsJsonAsync("api/Clientes/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                // Dizemos ao leitor de JSON para usar nossas novas opções
                return await response.Content.ReadFromJsonAsync<ClienteLoginResponseDto>(_jsonOptions);
            }
            return null;
        }

        // --- MÉTODO DE CRIAR CHAMADO (ATUALIZADO) ---
        public async Task<bool> CriarChamadoAsync(ChamadoCreateRequestDto chamadoRequest)
        {
            var client = _httpClientFactory.CreateClient("HelpDeskApi");
            var response = await client.PostAsJsonAsync("api/Chamados", chamadoRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            string mensagemErro = "A API recusou a solicitação de criação do chamado.";
            try
            {
                // Dizemos ao leitor de JSON para usar nossas novas opções
                var erroContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>(_jsonOptions);
                if (erroContent != null && erroContent.ContainsKey("message"))
                {
                    mensagemErro = erroContent["message"];
                }
            }
            catch { mensagemErro = $"Erro da API: {response.StatusCode}"; }

            throw new Exception(mensagemErro);
        }

        // --- MÉTODO DE BUSCAR CHAMADOS (ATUALIZADO) ---
        // (Este foi o método que causou o erro que você viu no print)
        public async Task<List<ChamadoDto>> GetChamadosPorClienteAsync(int clienteId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("HelpDeskApi");

                // Dizemos ao leitor de JSON para usar nossas novas opções
                var chamados = await client.GetFromJsonAsync<List<ChamadoDto>>($"api/Chamados/cliente/{clienteId}", _jsonOptions);

                return chamados ?? new List<ChamadoDto>();
            }
            catch (Exception ex)
            {
                // Agora, se o erro persistir (o que não deve), a mensagem será mais clara
                throw new Exception($"Não foi possível carregar o histórico: {ex.Message}");
            }
        }

        public async Task<ChatbotResponseDto> ProcessarMensagemAsync(ChatbotRequestDto request)
        {
            var client = _httpClientFactory.CreateClient("HelpDeskApi");

            // 1. Envia a mensagem do usuário para a API (usando _jsonOptions para os Enums)
            var response = await client.PostAsJsonAsync("api/Chatbot/processar", request, _jsonOptions);

            // 2. Se a API falhar, joga um erro
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("O assistente virtual está temporariamente indisponível.");
            }

            // 3. Lê a resposta da API (usando _jsonOptions) e a retorna para a página
            var chatbotResponse = await response.Content.ReadFromJsonAsync<ChatbotResponseDto>(_jsonOptions);

            return chatbotResponse ?? throw new Exception("Resposta inválida do assistente.");
        }
    }
}