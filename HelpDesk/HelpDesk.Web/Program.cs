// Local: HelpDesk.Web/Program.cs

using HelpDesk.Web;
using HelpDesk.Web.Components;
using HelpDesk.Web.Components.Layout;
using MudBlazor.Services;
using HelpDesk.Web.Services; // 'using' para o IApiClient

var builder = WebApplication.CreateBuilder(args);

// --- 1. Adiciona os serviços essenciais ao contêiner. ---

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

// (CORREÇÃO) Configura o HttpClient NOMEADO para se comunicar com a API
builder.Services.AddHttpClient("HelpDeskApi", client =>
{
    // A porta foi corrigida de 7113 para 7133,
    // conforme o seu arquivo launchSettings.json da API.
    client.BaseAddress = new Uri("https://localhost:7133");
});

// Registra o ApiClient para que ele possa ser injetado nas páginas.
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