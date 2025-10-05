// Local: HelpDesk.Shared/Dtos/ContratoDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ContratoDto
    {
        public int IdContrato { get; set; }
        public string TipoPlano { get; set; } = string.Empty;
        public string DescricaoPlano { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
    }
}