using BancoNix.Dominio;
using BancoNix.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BancoNix.Infra.EF.Configurations
{
    public class TransferenciaConfiguration : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder
                .HasKey(t => t.Id)
                .HasName("id");

            builder
                .Property(t => t.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(15,2)");

            builder
                .Property(t => t.Data)
                .HasColumnName("data_criacao");

            builder.Property(t => t.Status)
                    .HasConversion(
                        v => v.ToString(),
                        v => (Status)Enum.Parse(typeof(Status), v))
                    .HasColumnName("status");

            builder.Property(t => t.Tipo)
                    .HasConversion(
                        v => v.ToString(),
                        v => (Tipo)Enum.Parse(typeof(Tipo), v))
                    .HasColumnName("tipo");

            builder.OwnsOne(t => t.Pagador, p =>
            {
                p.Property(x => x.Nome).HasColumnName("pagador_nome");
                p.Property(x => x.Banco).HasColumnName("pagador_banco");
                p.Property(x => x.Agencia).HasColumnName("pagador_agencia");
                p.Property(x => x.Conta).HasColumnName("pagador_conta");
            });

            builder.OwnsOne(t => t.Beneficiario, b =>
            {
                b.Property(x => x.Nome).HasColumnName("beneficiario_nome");
                b.Property(x => x.Banco).HasColumnName("beneficiario_banco");
                b.Property(x => x.Agencia).HasColumnName("beneficiario_agencia");
                b.Property(x => x.Conta).HasColumnName("beneficiario_conta");
            });

            builder
                .HasOne(x => x.Usuario)
                .WithMany(x => x.Transferencias)
                .HasForeignKey(x => x.UsuarioId);
        }
    }
}
