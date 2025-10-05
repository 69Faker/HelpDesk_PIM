// Local: HelpDesk.Shared/Dtos/ChamadoDetailDto.cs
using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    public class ChamadoDetailDto : ChamadoDto // Herda de ChamadoDto
    {
        public CategoriaChamado Categoria { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime? DataFechamento { get; set; }
        public List<MensagemDto> Mensagens { get; set; } = new List<MensagemDto>();
    }
}