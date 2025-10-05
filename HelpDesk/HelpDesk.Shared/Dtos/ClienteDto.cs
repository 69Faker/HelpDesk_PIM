// Local: HelpDesk.Shared/Dtos/ClienteDto.cs
namespace HelpDesk.Shared.Dtos
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;

        // Adicionamos os detalhes do contrato aqui
        public ContratoDto? Contrato { get; set; }
    }
}