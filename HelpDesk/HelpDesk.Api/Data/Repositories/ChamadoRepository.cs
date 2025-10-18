using HelpDesk.Api.Data;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Data.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly HelpDeskDbContext _context;

        public ChamadoRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chamado>> GetAllAsync()
        {
            // .Include() traz os dados do Cliente relacionado
            return await _context.Chamados
                .Include(c => c.Cliente)
                .AsNoTracking() // Boa prática para listas de leitura
                .ToListAsync();
        }

        public async Task<Chamado?> GetByIdAsync(int id)
        {
            // .Include() traz os dados relacionados (Cliente e Mensagens)
            return await _context.Chamados
                .Include(c => c.Cliente)
                .Include(c => c.Mensagens)
                .FirstOrDefaultAsync(c => c.IdChamado == id);
        }

        public async Task UpdateAsync(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }
    }
}