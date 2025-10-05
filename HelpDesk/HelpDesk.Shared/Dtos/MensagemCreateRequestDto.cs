// Local: HelpDesk.Shared/Dtos/MensagemCreateRequestDto.cs
using HelpDesk.Shared.Enums;

namespace HelpDesk.Shared.Dtos
{
    public class MensagemCreateRequestDto
    {
        public string Texto { get; set; } = string.Empty;
        public AutorMensagem Autor { get; set; }
    }
}