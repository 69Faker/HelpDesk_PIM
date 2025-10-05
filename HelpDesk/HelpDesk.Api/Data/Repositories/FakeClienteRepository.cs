using HelpDesk.Api.Models;

namespace HelpDesk.Api.Data.Repositories
{
    public class FakeClienteRepository : IClienteRepository
    {
        // Vamos criar um cliente de exemplo para poder testar o login
        private readonly List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente { IdCliente = 1, Nome = "Usuario Teste", CPF = "12345678900", Email = "teste@teste.com", SenhaHash = "senha123" } // ATENÇÃO: Senha em texto plano apenas para o mock!
        };

        public Task<Cliente?> GetByCpfAsync(string cpf)
        {
            var cliente = _clientes.FirstOrDefault(c => c.CPF == cpf);
            return Task.FromResult(cliente);
        }

        public Task<Cliente?> GetByIdAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.IdCliente == id);
            return Task.FromResult(cliente);
        }

        public Task UpdateAsync(Cliente cliente)
        {
            // Em um mock simples, podemos não fazer nada aqui ou implementar uma lógica de atualização na lista.
            var clienteExistente = _clientes.FirstOrDefault(c => c.IdCliente == cliente.IdCliente);
            if (clienteExistente != null)
            {
                clienteExistente.SenhaHash = cliente.SenhaHash; // Exemplo de atualização
            }
            return Task.CompletedTask;
        }
    }
}