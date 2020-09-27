using BancoNix.Aplicacao;
using BancoNix.Aplicacao.Commands;
using BancoNix.Aplicacao.Filtros;
using BancoNix.Aplicacao.Models;
using BancoNix.Dominio;
using BancoNix.Dominio.Enums;
using BancoNix.Infra.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BancoNix.Tests.Aplicacao
{
    public class TransferenciaServiceTests
    {
        private readonly BancoContext _context;
        private readonly TransferenciaService _transferenciaService;

        public TransferenciaServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();

            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new BancoContext(optionsBuilder.Options);

            _transferenciaService = new TransferenciaService(_context);
        }

        [Fact]
        public async Task Retornar_verdadeiro_quando_criar_transferencia()
        {
            CriarTransferenciaCommand command = new CriarTransferenciaCommand
            {
                Beneficiario = new DadosTransacaoModel(),
                Pagador = new DadosTransacaoModel()
            };

            var retorno = await _transferenciaService.Criar(command);

            Assert.Equal(1, _context.Transferencias.Count());
            Assert.True(retorno);
        }

        [Fact]
        public async Task Retornar_verdadeiro_quando_remover_transferencia()
        {
            var dados = new DadosTransacao(string.Empty, string.Empty, string.Empty, string.Empty);
            await _context.Transferencias.AddAsync(new Transferencia(1, dados, dados, 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Remover(_context.Transferencias.First().Id);

            Assert.True(resultado);
            Assert.Equal(1, _context.Transferencias.Count());
        }

        [Fact]
        public async Task Retornar_falso_quando_tentar_remover_transferencia_inexistente()
        {
            var resultado = await _transferenciaService.Remover(1);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Retornar_transferencia()
        {
            var dados = new DadosTransacao(string.Empty, string.Empty, string.Empty, string.Empty);
            await _context.Usuarios.AddAsync(new Usuario(1, "empresa", "12345678901234"));
            await _context.Transferencias.AddAsync(new Transferencia(1, dados, dados, 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Buscar(_context.Transferencias.First().Id);

            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task Retornar_nulo_quando_nao_achar_transferencia()
        {
            var resultado = await _transferenciaService.Buscar(123);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Retornar_transferencia_com_filtro_por_nome_pagador()
        {
            var dados = new DadosTransacao("pagador", string.Empty, string.Empty, string.Empty);
            await _context.Usuarios.AddAsync(new Usuario(1, "empresa", "12345678901234"));
            await _context.Transferencias.AddAsync(new Transferencia(1, dados, new DadosTransacao("", "", "", ""), 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Buscar(new FiltroTransferencia { NomePagador = "pagador" });

            Assert.Single(resultado.Transferencias);
        }

        [Fact]
        public async Task Retornar_transferencia_com_filtro_por_nome_beneficiario()
        {
            var dados = new DadosTransacao("beneficiario", string.Empty, string.Empty, string.Empty);
            await _context.Usuarios.AddAsync(new Usuario(1, "empresa", "12345678901234"));
            await _context.Transferencias.AddAsync(new Transferencia(1, new DadosTransacao("", "", "", ""), dados, 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Buscar(new FiltroTransferencia { NomeBeneficiario = "beneficiario" });

            Assert.Single(resultado.Transferencias);
        }

        [Fact]
        public async Task Retornar_transferencia_com_filtro_por_tipo()
        {
            var dados = new DadosTransacao("beneficiario", string.Empty, string.Empty, string.Empty);
            await _context.Usuarios.AddAsync(new Usuario(1, "empresa", "12345678901234"));
            await _context.Transferencias.AddAsync(new Transferencia(1, new DadosTransacao("", "", "", ""), dados, 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Buscar(new FiltroTransferencia { Tipo = Tipo.CC });

            Assert.Single(resultado.Transferencias);
        }

        [Fact]
        public async Task Retornar_transferencia_com_filtro_por_status()
        {
            var dados = new DadosTransacao("beneficiario", string.Empty, string.Empty, string.Empty);
            await _context.Usuarios.AddAsync(new Usuario(1, "empresa", "12345678901234"));
            await _context.Transferencias.AddAsync(new Transferencia(1, new DadosTransacao("", "", "", ""), dados, 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Buscar(new FiltroTransferencia { Status = Status.OK });

            Assert.Single(resultado.Transferencias);
        }

        [Fact]
        public async Task Retornar_transferencia_com_filtro_por_data()
        {
            var dados = new DadosTransacao("beneficiario", string.Empty, string.Empty, string.Empty);
            await _context.Usuarios.AddAsync(new Usuario(1, "empresa", "12345678901234"));
            await _context.Transferencias.AddAsync(new Transferencia(1, new DadosTransacao("", "", "", ""), dados, 0));
            await _context.SaveChangesAsync();

            var resultado = await _transferenciaService.Buscar(new FiltroTransferencia { Data = _context.Transferencias.First().Data });

            Assert.Single(resultado.Transferencias);
        }

    }
}
