using HelpDesk.Web;
using HelpDesk.Web.Components.Layout; // Adicionamos para o Blazor saber onde estao os Layouts
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Adiciona os servi�os essenciais ao cont�iner. ---

// Habilita o Blazor Server e seus componentes
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adiciona os servi�os do MudBlazor (para os componentes de UI funcionarem)
builder.Services.AddMudServices();

// Configura o HttpClient para se comunicar com a nossa API
builder.Services.AddHttpClient("HelpDeskApi", client =>
{
    // IMPORTANTE: Esta deve ser a URL base onde sua API (HelpDesk.Api) est� rodando.
    // Verifique a porta no arquivo launchSettings.json do projeto da API.
    client.BaseAddress = new Uri("https://localhost:7001");
});


// TODO: Futuramente, registraremos nossos servi�os de aplica��o aqui
// builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IChamadoService, ChamadoService>();


var app = builder.Build();

// --- 2. Configura o pipeline de requisi��es HTTP. ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permite o uso de arquivos est�ticos (CSS, JS, imagens)
app.UseAntiforgery();

// Mapeia o componente raiz <App> e define o modo de renderiza��o
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// --- 3. Inicia a aplica��o. ---
app.Run();