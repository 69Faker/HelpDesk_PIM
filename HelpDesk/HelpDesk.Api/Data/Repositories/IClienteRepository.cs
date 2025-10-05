using HelpDesk.Api.Models;

namespace HelpDesk.Api.Data.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByCpfAsync(string cpf);
        Task<Cliente?> GetByIdAsync(int id);
        Task UpdateAsync(Cliente cliente);
        // ... outros métodos conforme a necessidade
    }
}