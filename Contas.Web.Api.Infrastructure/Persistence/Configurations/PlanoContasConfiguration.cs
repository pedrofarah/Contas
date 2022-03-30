using Contas.Web.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Web.Api.Infrastructure.Persistence.Configurations
{
    public class PlanoContasConfiguration : IEntityTypeConfiguration<PlanoContas>
    {
        public void Configure(EntityTypeBuilder<PlanoContas> builder)
        {
            builder
                .ToTable("TB_PLANOCONTAS");

            builder
                .Property(p => p.Codigo)
                .HasColumnName("CODIGO")
                .IsRequired();

            builder.HasKey(e => e.Codigo);

            builder
                .Property(p => p.Nome)
                .HasColumnName("NOME")
                .IsRequired();

            builder
                .Property(p => p.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            builder
                .Property(p => p.AceitaLancamentos)
                .HasColumnName("ACEITA_LANCAMENTOS")
                .IsRequired();

            builder
                .Property(p => p.ContaPai)
                .HasColumnName("CONTA_PAI");

        }
    }
}
