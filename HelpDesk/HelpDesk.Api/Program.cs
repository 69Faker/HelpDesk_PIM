using HelpDesk.Api.Data;
using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- INÍCIO DAS CONFIGURAÇÕES DE SERVIÇO ---

// 1. Adiciona o DbContext para o Entity Framework (ISSO JÁ ESTAVA CORRETO)
builder.Services.AddDbContext<HelpDeskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Isso força a API a LER e ESCREVER Enums como strings (ex: "OUTROS")
        // em vez de números (ex: 3). Isso corrige o bug que você achou.
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// 2. Registro dos nossos repositórios e serviços para injeção de dependência.
// --- ESSA É A MUDANÇA QUE FIZEMOS ---
// Trocamos o FakeClienteRepository pelo ClienteRepository real.
// Também mudei de AddSingleton para AddScoped, que é o correto para DbContext.
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>(); // Este já estava correto.

// Serviços
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IChatbotService, ChatbotService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. (CORREÇÃO) Adiciona a política de CORS (ISSO JÁ ESTAVA CORRETO)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy =>
        {
            // Para desenvolvimento, permite qualquer origem, método e header.
            // Em produção, você deve restringir isso para a URL do seu Blazor.
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// --- FIM DAS CONFIGURAÇÕES DE SERVIÇO ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 4. (CORREÇÃO) Habilita a política de CORS que criamos (ISSO JÁ ESTAVA CORRETO)
//    Deve vir ANTES de UseAuthorization e MapControllers.
app.UseCors("AllowBlazorApp");

app.UseAuthorization();

app.MapControllers();

app.Run();