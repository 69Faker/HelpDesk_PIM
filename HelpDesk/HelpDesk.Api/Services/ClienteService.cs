using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Models;
using System;
using System.Threading.Tasks;

namespace HelpDesk.Api.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente?> AutenticarAsync(string cpf, string senha)
        {
            var cliente = await _clienteRepository.GetByCpfAsync(cpf);

            if (cliente == null)
            {
                return null; // Cliente não encontrado
            }

            // !!!!! AVISO DE SEGURANÇA !!!!!
            // Em um projeto real, NUNCA compare senhas em texto plano.
            // Aqui você usaria uma biblioteca como BCrypt.Net para comparar o hash da senha.
            // Ex: if (!BCrypt.Net.BCrypt.Verify(senha, cliente.SenhaHash)) return null;
            if (cliente.SenhaHash != senha)
            {
                return null; // Senha incorreta
            }

            return cliente;
        }

        public async Task AlterarSenhaAsync(int clienteId, string senhaAntiga, string novaSenha)
        {
            var cliente = await _clienteRepository.GetByIdAsync(clienteId);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            // Aqui também ocorreria a verificação da senha antiga com hash
            if (cliente.SenhaHash != senhaAntiga)
            {
                throw new Exception("Senha antiga incorreta.");
            }

            // Aqui ocorreria a geração de um novo hash para a nova senha
            // Ex: cliente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
            cliente.SenhaHash = novaSenha;

            await _clienteRepository.UpdateAsync(cliente);
        }
    }
}