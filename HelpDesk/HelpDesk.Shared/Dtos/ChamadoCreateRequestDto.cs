using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    // Este DTO é usado tanto para a API propor um chamado
    // quanto para o front-end criar um chamado manualmente.
    public class ChamadoCreateRequestDto
    {
        // O ClienteId virá de outro lugar (do usuário logado ou do request principal)
        public int ClienteId { get; set; }

        public CategoriaChamado Categoria { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}