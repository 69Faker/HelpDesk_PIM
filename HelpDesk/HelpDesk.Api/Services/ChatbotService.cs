using HelpDesk.Api.Data.Repositories;
using HelpDesk.Api.Models;
using HelpDesk.Shared.Dtos;
using HelpDesk.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpDesk.Api.Services
{
    public class ChatbotService : IChatbotService
    {
        private static readonly Dictionary<string, string> _baseConhecimento = new Dictionary<string, string>
        {
            // ... (sua base de FAQ continua a mesma) ...
            { "senha", "Para redefinir sua senha, por favor, clique em 'Esqueci minha senha' na tela de login." },
            { "esqueci", "Para redefinir sua senha, por favor, clique em 'Esqueci minha senha' na tela de login." },
            { "rede", "Verifique se seu modem/roteador está ligado e se o cabo de rede está bem conectado. Tente reiniciar o modem e o computador." },
            { "internet", "Verifique se seu modem/roteador está ligado e se o cabo de rede está bem conectado. Tente reiniciar o modem e o computador." },
            { "lento", "Tente fechar outros aplicativos ou abas do navegador que não esteja usando. Se a lentidão persistir, reinicie seu computador." },
            { "pagamento", "Para assuntos financeiros ou de pagamento, por favor, abra um chamado manualmente na categoria 'Pagamento' para que nosso time especializado possa te ajudar." },
            { "instalação", "Assuntos sobre instalação são tratados pela nossa equipe. Vou abrir um chamado para você sobre isso." },
            { "troca", "Assuntos sobre troca de equipamento são tratados pela nossa equipe. Vou abrir um chamado para você sobre isso." }
        };

        private readonly IChamadoRepository _chamadoRepo;
        private readonly IClienteRepository _clienteRepo;

        public ChatbotService(IChamadoRepository chamadoRepo, IClienteRepository clienteRepo)
        {
            _chamadoRepo = chamadoRepo;
            _clienteRepo = clienteRepo;
        }

        public async Task<ChatbotResponseDto> ProcessarMensagemAsync(ChatbotRequestDto request)
        {
            var mensagemLower = request.Mensagem.ToLower();

            // --- FLUXO 1: O USUÁRIO ESTÁ RESPONDENDO A UMA CONFIRMAÇÃO ---
            if (request.PropostaPendente != null)
            {
                // O usuário enviou a proposta de volta. Vamos ver se ele confirmou.
                if (mensagemLower.Contains("sim") || mensagemLower.Contains("confirmo") || mensagemLower.Contains("ok") || mensagemLower.Contains("certo"))
                {
                    // O usuário confirmou! Vamos criar o chamado.
                    var cliente = await _clienteRepo.GetByIdAsync(request.ClienteId);
                    if (cliente == null)
                        return new ChatbotResponseDto { Resposta = "Erro: Cliente não encontrado.", Tipo = TipoRespostaChatbot.Erro };

                    // Mapeia o DTO (proposta) para o Modelo (entidade do banco)
                    var novoChamado = new Chamado
                    {
                        ClienteId = request.ClienteId,
                        Titulo = request.PropostaPendente.Titulo,
                        Descricao = request.PropostaPendente.Descricao,
                        Categoria = request.PropostaPendente.Categoria,
                        DataAbertura = DateTime.Now,
                        Status = StatusChamado.ABERTO
                    };

                    await _chamadoRepo.AddAsync(novoChamado); // Salva no banco

                    return new ChatbotResponseDto
                    {
                        Resposta = $"Pronto! Abri o chamado para você. O número do seu ticket é {novoChamado.IdChamado}.",
                        Tipo = TipoRespostaChatbot.ChamadoCriado,
                        ChamadoId = novoChamado.IdChamado
                    };
                }
                else
                {
                    // O usuário não confirmou (disse "não", "cancelar", etc.)
                    return new ChatbotResponseDto
                    {
                        Resposta = "Entendido. Abertura de chamado cancelada. Posso ajudar em algo mais?",
                        Tipo = TipoRespostaChatbot.Faq // Volta ao fluxo normal
                    };
                }
            }

            // --- FLUXO 2: MENSAGEM NOVA (BUSCAR NA FAQ) ---
            foreach (var (palavraChave, resposta) in _baseConhecimento)
            {
                if (mensagemLower.Contains(palavraChave))
                {
                    return new ChatbotResponseDto { Resposta = resposta, Tipo = TipoRespostaChatbot.Faq };
                }
            }

            // --- FLUXO 3: NÃO ACHOU NA FAQ (PROPOR UM NOVO CHAMADO) ---
            var clienteCheck = await _clienteRepo.GetByIdAsync(request.ClienteId);
            if (clienteCheck == null)
                return new ChatbotResponseDto { Resposta = "Erro: Cliente não encontrado.", Tipo = TipoRespostaChatbot.Erro };

            // Tenta adivinhar a categoria
            var categoria = DefinirCategoria(mensagemLower);

            // Cria a PROPOSTA
            var proposta = new ChamadoCreateRequestDto
            {
                Titulo = "Novo Chamado via Chatbot",
                Descricao = request.Mensagem,
                Categoria = categoria
            };

            // Envia a proposta para confirmação
            return new ChatbotResponseDto
            {
                Resposta = $"Não encontrei uma resposta pronta na minha base.\n\nPosso abrir um chamado para você com os seguintes dados?\n- Assunto: {proposta.Titulo}\n- Descrição: {proposta.Descricao}\n- Categoria: {proposta.Categoria}\n\nResponda 'sim' para confirmar.",
                Tipo = TipoRespostaChatbot.ConfirmacaoChamado,
                PropostaChamado = proposta // Envia a proposta para o front-end
            };
        }

        private CategoriaChamado DefinirCategoria(string mensagemLower)
        {
            if (mensagemLower.Contains("instalaç") || mensagemLower.Contains("instalar"))
                return CategoriaChamado.DATA_DE_INSTALACAO;
            if (mensagemLower.Contains("equipamento") || mensagemLower.Contains("troca"))
                return CategoriaChamado.TROCA_DE_EQUIPAMENTOS;
            if (mensagemLower.Contains("rede") || mensagemLower.Contains("internet") || mensagemLower.Contains("wifi"))
                return CategoriaChamado.PROBLEMAS_DE_REDE;

            return CategoriaChamado.OUTROS;
        }
    }
}