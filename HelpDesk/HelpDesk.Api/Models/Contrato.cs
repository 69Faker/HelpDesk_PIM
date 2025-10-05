namespace HelpDesk.Api.Models
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public string TipoPlano { get; set; } = string.Empty;
        public string DescricaoPlano { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
    }
}