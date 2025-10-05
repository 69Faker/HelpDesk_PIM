namespace HelpDesk.Api.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        public int ContratoId { get; set; }
        public virtual Contrato? Contrato { get; set; } // Pode ser nulo até ser carregado

        public virtual ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}