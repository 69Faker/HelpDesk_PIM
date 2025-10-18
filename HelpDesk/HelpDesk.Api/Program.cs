using HelpDesk.Api.Data; // Adicionado
using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Services;
using Microsoft.EntityFrameworkCore; // Adicionado

var builder = WebApplication.CreateBuilder(args);

// --- INÍCIO DAS CONFIGURAÇÕES DE SERVIÇO ---

// 1. Adiciona o DbContext para o Entity Framework
builder.Services.AddDbContext<HelpDeskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllers();

// 2. Registro dos nossos repositórios e serviços para injeção de dependência.
//    Mudamos de 'Singleton' para 'Scoped' para funcionar corretamente com o DbContext.
//    'Scoped' significa que uma nova instância é criada para cada requisição HTTP.

// Comente ou remova os repositórios "Fake"
// builder.Services.AddSingleton<IChamadoRepository, FakeChamadoRepository>();
// builder.Services.AddSingleton<IClienteRepository, FakeClienteRepository>();

// Adicione as implementações "Reais"
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Serviços (também devem ser 'Scoped' ou 'Transient' se dependerem de repositórios 'Scoped')
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IChatbotService, ChatbotService>();
// ***** FIM DO BLOCO ADICIONADO/MODIFICADO *****

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- FIM DAS CONFIGURAÇÕES DE SERVIÇO ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();