// Arquivo: IChamadoService.cs

using HelpDesk.Api.Models;
using HelpDesk.Shared.Enums;

namespace HelpDesk.Api.Data.Repositories
{
    public interface IChamadoService // Apenas a DEFINIÇÃO da interface
    {
        Task<IEnumerable<Chamado>> GetChamadosPorClienteAsync(int clienteId);
        Task<Chamado?> GetChamadoByIdAsync(int chamadoId);
        Task<Chamado> AbrirChamadoAsync(int clienteId, CategoriaChamado categoria, string titulo, string descricao);
        Task AdicionarMensagemAsync(int chamadoId, Mensagem novaMensagem);
        Task CancelarChamadoAsync(int chamadoId);
    }
}