// Local: HelpDesk.Shared/Dtos/ChamadoDto.cs
using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    public class ChamadoDto
    {
        public int IdChamado { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public StatusChamado Status { get; set; }
        public int ClienteId { get; set; }
    }
}