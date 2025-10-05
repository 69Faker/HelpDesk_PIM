// Local: HelpDesk.Shared/Dtos/MensagemDto.cs
using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    public class MensagemDto
    {
        public int IdMensagem { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; }
        public AutorMensagem Autor { get; set; }
    }
}