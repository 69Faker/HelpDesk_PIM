using HelpDesk.Shared.Enums;

namespace HelpDesk.Api.Models
{
    public class Chamado
    {
        public int IdChamado { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; } // Pode ser nulo se não foi fechado
        public StatusChamado Status { get; set; }

        // Propriedades de Navegação (Relações)
        public int ClienteId { get; set; } // Chave estrangeira para Cliente
        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<Mensagem> Mensagens { get; set; } = new List<Mensagem>();
    }
}