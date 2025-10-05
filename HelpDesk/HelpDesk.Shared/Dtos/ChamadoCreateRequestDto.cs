// Local: HelpDesk.Shared/Dtos/ChamadoCreateRequestDto.cs
using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    public class ChamadoCreateRequestDto
    {
        // O ID do cliente virá da rota ou do token de autenticação no futuro,
        // mas por agora vamos recebê-lo na requisição.
        public int ClienteId { get; set; }
        public CategoriaChamado Categoria { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}