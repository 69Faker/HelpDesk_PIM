// Local: HelpDesk.Shared/Dtos/ChamadoCreateRequestDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ChamadoCreateRequestDto
    {
        // O ID do cliente vir� da rota ou do token de autentica��o no futuro,
        // mas por agora vamos receb�-lo na requisi��o.
        public int ClienteId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}