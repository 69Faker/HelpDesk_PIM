// Local: Program.cs (Seu arquivo ATUALIZADO)

using HelpDesk.Web.Components.Layout;
using MudBlazor.Services;
using HelpDesk.Web.Services; // <--- ADICIONE ESTE 'using'

var builder = WebApplication.CreateBuilder(args);

// --- 1. Adiciona os serviços essenciais ao contêiner. ---

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

// Configura o HttpClient NOMEADO para se comunicar com a nossa API
builder.Services.AddHttpClient("HelpDeskApi", client =>
{
    // IMPORTANTE: Esta é a SUA URL. (Porta 7001)
    // Está correto e alinhado com o que você já tinha.
    client.BaseAddress = new Uri("https://localhost:7001");
});

// --- ESTA É A LINHA QUE FALTAVA ---
// Registra o ApiClient para que ele possa ser injetado nas páginas.
// Ele usará o HttpClientFactory para obter o "HelpDeskApi" que você 
// configurou acima.
builder.Services.AddScoped<IApiClient, ApiClient>();


var app = builder.Build();

// --- 2. Configura o pipeline de requisições HTTP. ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// --- 3. Inicia a aplicação. ---
app.Run();