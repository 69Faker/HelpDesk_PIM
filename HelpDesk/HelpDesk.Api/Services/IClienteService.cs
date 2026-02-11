using HelpDesk.Api.Models;

namespace HelpDesk.Api.Services
{
    public interface IClienteService
    {
        Task<Cliente?> AutenticarAsync(string cpf, string senha);
        Task AlterarSenhaAsync(int clienteId, string senhaAntiga, string novaSenha);

        Task RedefinirSenhaAsync(string cpf, string email, string novaSenha);
    }
}