// Arquivo: ChamadoService.cs

using HelpDesk.Shared.Enums;
using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Models;
using HelpDesk.Api.Services;

namespace HelpDesk.Api.Services
{
    public class ChamadoService : IChamadoService // A classe IMPLEMENTA a interface
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IClienteRepository _clienteRepository;

        // Injetamos os dois repositórios que nosso serviço precisa para trabalhar
        public ChamadoService(IChamadoRepository chamadoRepository, IClienteRepository clienteRepository)
        {
            _chamadoRepository = chamadoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<Chamado> AbrirChamadoAsync(int clienteId, CategoriaChamado categoria,string titulo, string descricao)
        {
            // Regra de negócio: Verifica se o cliente existe antes de abrir o chamado
            var cliente = await _clienteRepository.GetByIdAsync(clienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado. Não é possível abrir o chamado.");
            }

            var novoChamado = new Chamado
            {
                ClienteId = clienteId,
                Categoria = categoria,
                Titulo = titulo,
                Descricao = descricao,
                DataAbertura = DateTime.UtcNow,
                Status = StatusChamado.ABERTO
            };

            await _chamadoRepository.AddAsync(novoChamado);
            return novoChamado;
        }

        public async Task AdicionarMensagemAsync(int chamadoId, Mensagem novaMensagem)
        {
            var chamado = await _chamadoRepository.GetByIdAsync(chamadoId);
            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            // Regra de negócio: Não permite adicionar mensagens em chamados finalizados ou cancelados
            if (chamado.Status == StatusChamado.FINALIZADO || chamado.Status == StatusChamado.CANCELADO)
            {
                throw new Exception("Não é possível adicionar mensagens a um chamado que já foi finalizado ou cancelado.");
            }

            novaMensagem.DataEnvio = DateTime.UtcNow;
            chamado.Mensagens.Add(novaMensagem);
            await _chamadoRepository.UpdateAsync(chamado);
        }

        public async Task<Chamado?> GetChamadoByIdAsync(int chamadoId)
        {
            return await _chamadoRepository.GetByIdAsync(chamadoId);
        }

        public async Task<IEnumerable<Chamado>> GetChamadosPorClienteAsync(int clienteId)
        {
            var todosOsChamados = await _chamadoRepository.GetAllAsync();
            return todosOsChamados.Where(c => c.ClienteId == clienteId);
        }
        public async Task CancelarChamadoAsync(int chamadoId)
        {
            var chamado = await _chamadoRepository.GetByIdAsync(chamadoId);
            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            // Regra de Negócio: Não se pode cancelar um chamado que já foi finalizado.
            if (chamado.Status == StatusChamado.FINALIZADO)
            {
                throw new Exception("Não é possível cancelar um chamado que já foi finalizado.");
            }

            // Regra de Negócio: Se o chamado já está cancelado, não fazemos nada.
            if (chamado.Status == StatusChamado.CANCELADO)
            {
                return; // Já está cancelado, operação bem-sucedida.
            }

            chamado.Status = StatusChamado.CANCELADO;
            chamado.DataFechamento = DateTime.UtcNow;

            await _chamadoRepository.UpdateAsync(chamado);
        }

    }
}