// Local: HelpDesk.Shared/Dtos/ChamadoDetailDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ChamadoDetailDto : ChamadoDto // Herda de ChamadoDto
    {
        public string Descricao { get; set; } = string.Empty;
        public DateTime? DataFechamento { get; set; }
        public List<MensagemDto> Mensagens { get; set; } = new List<MensagemDto>();
    }
}