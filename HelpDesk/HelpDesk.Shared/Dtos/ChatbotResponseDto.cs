using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    public class ChatbotResponseDto
    {
        public string Resposta { get; set; } = string.Empty;
        public TipoRespostaChatbot Tipo { get; set; }
        public int? ChamadoId { get; set; }

        // NOVO CAMPO:
        // Se o tipo for 'ConfirmacaoChamado', este campo
        // conterá os detalhes da proposta para o front-end mostrar.
        public ChamadoCreateRequestDto? PropostaChamado { get; set; }
    }
}