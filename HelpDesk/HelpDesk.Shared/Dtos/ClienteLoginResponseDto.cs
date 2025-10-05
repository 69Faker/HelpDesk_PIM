// Local: HelpDesk.Shared/Dtos/ClienteLoginResponseDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ClienteLoginResponseDto
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Futuramente, poderíamos adicionar um Token JWT aqui.
    }
}