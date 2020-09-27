using BancoNix.Dominio;
using BancoNix.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace BancoNix.Infra.EF
{
    public class BancoContext : DbContext
    {
        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public BancoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {
            Usuarios.Add(new Usuario(1, "Empresa Star Wars", "56478803000100"));
            Usuarios.Add(new Usuario(2, "Empresa Star Trek", "99975891000169"));
            Usuarios.Add(new Usuario(3, "Empresa Marvel", "75846915000111"));
            Usuarios.Add(new Usuario(4, "Empresa DC", "95327005000130"));
            Usuarios.Add(new Usuario(5, "Empresa Jones", "19166917000199"));

            // CC
            Transferencias.Add(
                new Transferencia(
                    1, 
                    new DadosTransacao("Pagador 1", "BancoNix", "001", "15185"),
                    new DadosTransacao ("Beneficiario 1", "BancoNix", "001", "23454"),
                    800.56
                    ));

            // status erro
            Transferencias.Add(
              new Transferencia(
                  2,
                  new DadosTransacao("Pagador 2", "BancoNix", "001", "15185"),
                  new DadosTransacao("Beneficiario 2", "BancoNix", "001", "23454"),
                  105000.47
                  ));

            // TED
            Transferencia ted = new Transferencia(
                  3,
                  new DadosTransacao("Pagador 3", "BancoNix", "001", "15185"),
                  new DadosTransacao("Beneficiario 3", "Santander", "001", "23454"),
                  500.00
                  );
            ted.Tipo = Tipo.TED;
            Transferencias.Add(ted);

            // DOC
            Transferencias.Add(
                new Transferencia(
                    4,
                    new DadosTransacao("Pagador 4", "BancoNix", "001", "15185"),
                    new DadosTransacao("Beneficiario 4", "BancoNix", "001", "23454"),
                    5500.90
                    ));

            // Removida
            Transferencia removida = new Transferencia(
                    5,
                    new DadosTransacao("Pagador 5", "Inter", "001", "15185"),
                    new DadosTransacao("Beneficiario 5", "Nubank", "001", "23454"),
                    4800.56
                    );
            removida.Removida = true;
            Transferencias.Add(removida);

            SaveChanges();
        }
    }

    public class BancoContextFactory : IDesignTimeDbContextFactory<BancoContext>
    {
        public BancoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return new BancoContext(optionsBuilder.Options);
        }
    }
}
