using BancoNix.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoNix.Infra.EF.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .HasKey(u => u.Id)
                .HasName("id");

            builder
                .Property(u => u.Nome)
                .HasColumnName("nome")
                .HasMaxLength(128);

            builder
                .Property(x => x.Cnpj)
                .HasColumnName("cnpj")
                .HasMaxLength(14);
        }
    }
}
