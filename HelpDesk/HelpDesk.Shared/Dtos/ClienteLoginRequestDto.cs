// Local: HelpDesk.Shared/Dtos/ClienteLoginRequestDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ClienteLoginRequestDto
    {
        public string Cpf { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}