using HelpDesk.Api.Models;

namespace HelpDesk.Api.Data.Repositories
{
    public interface IChamadoRepository
    {
        Task<IEnumerable<Chamado>> GetAllAsync();
        Task<Chamado?> GetByIdAsync(int id);
        Task AddAsync(Chamado chamado);

        Task UpdateAsync(Chamado chamado);
        // ... outros métodos que você precisar (Update, Delete, etc.)
    }
}