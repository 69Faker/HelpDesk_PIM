using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Data
{
    public class HelpDeskDbContext : DbContext
    {
        public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options) : base(options)
        {
        }

        // Mapeia suas classes para tabelas no banco de dados
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações e Relacionamentos

            // Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.IdCliente);
                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.CPF).IsRequired().HasMaxLength(11);

                // Garante que o CPF seja único no banco
                entity.HasIndex(c => c.CPF).IsUnique();

                // Relacionamento 1-para-1: Cliente -> Contrato
                entity.HasOne(c => c.Contrato)
                      .WithMany() // Se o Contrato não precisar de uma lista de Clientes
                      .HasForeignKey(c => c.ContratoId);

                // Relacionamento 1-para-N: Cliente -> Chamados
                entity.HasMany(c => c.Chamados)
                      .WithOne(ch => ch.Cliente)
                      .HasForeignKey(ch => ch.ClienteId);
            });

            // Chamado
            modelBuilder.Entity<Chamado>(entity =>
            {
                entity.HasKey(ch => ch.IdChamado);
                entity.Property(ch => ch.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(ch => ch.Descricao).IsRequired();

                // Relacionamento 1-para-N: Chamado -> Mensagens
                entity.HasMany(ch => ch.Mensagens)
                      .WithOne(m => m.Chamado)
                      .HasForeignKey(m => m.ChamadoId);
            });

            // Contrato
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.HasKey(c => c.IdContrato);
                entity.Property(c => c.TipoPlano).IsRequired().HasMaxLength(50);
            });

            // Mensagem
            modelBuilder.Entity<Mensagem>(entity =>
            {
                entity.HasKey(m => m.IdMensagem);
                entity.Property(m => m.Texto).IsRequired();
            });
        }
    }
}