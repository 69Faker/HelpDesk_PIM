namespace HelpDesk.Shared.Dtos
{
    public class ChatbotRequestDto
    {
        public int ClienteId { get; set; }
        public string Mensagem { get; set; } = string.Empty;

        // NOVO CAMPO:
        // Se o usuário estiver respondendo a uma proposta,
        // o front-end deve enviar a proposta de volta aqui.
        public ChamadoCreateRequestDto? PropostaPendente { get; set; }
    }
}