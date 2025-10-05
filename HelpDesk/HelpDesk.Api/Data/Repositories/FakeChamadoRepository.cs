using HelpDesk.Api.Models;
using HelpDesk.Shared.Enums;

namespace HelpDesk.Api.Data.Repositories
{
    public class FakeChamadoRepository : IChamadoRepository
    {
        // Nossa "tabela" de chamados falsa, em memória.
        private readonly List<Chamado> _chamados = new List<Chamado>();
        private int _nextId = 1;

        public Task<IEnumerable<Chamado>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Chamado>>(_chamados);
        }

        public Task<Chamado?> GetByIdAsync(int id)
        {
            var chamado = _chamados.FirstOrDefault(c => c.IdChamado == id);
            return Task.FromResult(chamado);
        }

        public Task AddAsync(Chamado chamado)
        {
            chamado.IdChamado = _nextId++;
            chamado.DataAbertura = DateTime.UtcNow;
            chamado.Status = StatusChamado.ABERTO;
            _chamados.Add(chamado);
            return Task.CompletedTask;
        }

        // ***** MÉTODO ADICIONADO *****
        public Task UpdateAsync(Chamado chamado)
        {
            var itemExistente = _chamados.FirstOrDefault(c => c.IdChamado == chamado.IdChamado);
            if (itemExistente != null)
            {
                // Em uma implementação real, você atualizaria as propriedades.
                // Em nosso mock, vamos simplesmente remover o antigo e adicionar o novo para simular a atualização.
                _chamados.Remove(itemExistente);
                _chamados.Add(chamado);
            }
            return Task.CompletedTask;
        }
    }
}