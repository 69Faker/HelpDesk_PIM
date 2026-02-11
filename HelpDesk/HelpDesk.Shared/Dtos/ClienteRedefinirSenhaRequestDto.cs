// Local: HelpDesk.Shared/Dtos/ClienteRedefinirSenhaRequestDto.cs

namespace HelpDesk.Shared.Dtos
{
    public class ClienteRedefinirSenhaRequestDto
    {
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}