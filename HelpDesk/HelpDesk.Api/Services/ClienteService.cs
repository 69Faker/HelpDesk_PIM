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

            // --- CORREÇÃO APLICADA ---
            // Usamos .Trim() para remover espaços em branco que o tipo 'char' do banco adiciona.
            if (cliente.SenhaHash.Trim() != senha)
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

            // --- CORREÇÃO APLICADA ---
            // Usamos .Trim() para remover espaços em branco que o tipo 'char' do banco adiciona.
            if (cliente.SenhaHash.Trim() != senhaAntiga)
            {
                throw new Exception("Senha antiga incorreta.");
            }

            // Aqui ocorreria a geração de um novo hash para a nova senha
            // Ex: cliente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
            cliente.SenhaHash = novaSenha;

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task RedefinirSenhaAsync(string cpf, string email, string novaSenha)
        {
            // 1. Busca o cliente pelo CPF (que é único)
            var cliente = await _clienteRepository.GetByCpfAsync(cpf);

            // 2. Valida se o cliente existe
            if (cliente == null)
            {
                // Mensagem de erro genérica por segurança
                throw new Exception("CPF ou E-mail inválidos.");
            }

            // 3. Valida se o e-mail bate (usando ToLower() para não diferenciar maiúsculas)
            if (cliente.Email.Trim().ToLower() != email.Trim().ToLower())
            {
                // Mensagem de erro genérica por segurança
                throw new Exception("CPF ou E-mail inválidos.");
            }

            // 4. Se ambos (CPF e Email) baterem, atualiza a senha
            // (Em um app real, aqui você faria o HASH da novaSenha)
            cliente.SenhaHash = novaSenha;

            // 5. Salva a alteração no banco
            await _clienteRepository.UpdateAsync(cliente);
        }
    }
}