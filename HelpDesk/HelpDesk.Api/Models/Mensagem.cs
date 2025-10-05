using HelpDesk.Shared.Enums;

namespace HelpDesk.Api.Models
{
    public class Mensagem
    {
        public int IdMensagem { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; }
        public AutorMensagem Autor { get; set; }

        // Propriedades de Navegação (Relações)
        public int ChamadoId { get; set; } // Chave estrangeira para Chamado
        public virtual Chamado? Chamado { get; set; }
    }
}