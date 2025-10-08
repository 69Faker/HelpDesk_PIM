using HelpDesk.Web;
using HelpDesk.Web.Components.Layout; // Adicionamos para o Blazor saber onde estao os Layouts
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Adiciona os serviços essenciais ao contêiner. ---

// Habilita o Blazor Server e seus componentes
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adiciona os serviços do MudBlazor (para os componentes de UI funcionarem)
builder.Services.AddMudServices();

// Configura o HttpClient para se comunicar com a nossa API
builder.Services.AddHttpClient("HelpDeskApi", client =>
{
    // IMPORTANTE: Esta deve ser a URL base onde sua API (HelpDesk.Api) está rodando.
    // Verifique a porta no arquivo launchSettings.json do projeto da API.
    client.BaseAddress = new Uri("https://localhost:7001");
});


// TODO: Futuramente, registraremos nossos serviços de aplicação aqui
// builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IChamadoService, ChamadoService>();


var app = builder.Build();

// --- 2. Configura o pipeline de requisições HTTP. ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permite o uso de arquivos estáticos (CSS, JS, imagens)
app.UseAntiforgery();

// Mapeia o componente raiz <App> e define o modo de renderização
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// --- 3. Inicia a aplicação. ---
app.Run();