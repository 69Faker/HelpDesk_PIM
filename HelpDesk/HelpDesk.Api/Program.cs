using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// ***** INÍCIO DO BLOCO ADICIONADO *****
// Registro dos nossos repositórios e serviços para injeção de dependência.
// Para cada "interface", estamos dizendo qual "classe concreta" deve ser usada.
builder.Services.AddSingleton<IChamadoRepository, FakeChamadoRepository>();
builder.Services.AddSingleton<IClienteRepository, FakeClienteRepository>();
builder.Services.AddSingleton<IClienteService, ClienteService>();
builder.Services.AddSingleton<IChamadoService, ChamadoService>();
// ***** FIM DO BLOCO ADICIONADO *****

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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