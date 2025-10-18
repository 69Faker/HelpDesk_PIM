using HelpDesk.Api.Data;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly HelpDeskDbContext _context;

        public ClienteRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetByCpfAsync(string cpf)
        {
            // .Include() traz os dados do Contrato relacionado
            return await _context.Clientes
                .Include(c => c.Contrato)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            // .Include() traz os dados relacionados (Contrato e Chamados)
            return await _context.Clientes
                .Include(c => c.Contrato)
                .Include(c => c.Chamados)
                .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }
    }
}