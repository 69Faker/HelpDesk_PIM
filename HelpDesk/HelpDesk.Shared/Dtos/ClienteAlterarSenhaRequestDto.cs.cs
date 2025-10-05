// Local: HelpDesk.Shared/Dtos/ClienteAlterarSenhaRequestDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ClienteAlterarSenhaRequestDto
    {
        public string SenhaAntiga { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}