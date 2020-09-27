using BancoNix.Dominio;
using BancoNix.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BancoNix.Tests.Dominio
{
    public class TransferenciaTests
    {
        private readonly DadosTransacao _dados;

        public TransferenciaTests()
        {
            _dados = new DadosTransacao(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        [Fact]
        public void Criar_transferencia_valida()
        {
            Transferencia transferencia = new Transferencia(1, _dados, _dados, 100);

            Assert.Equal(Status.OK, transferencia.Status);
        }

        [Fact]
        public void Criar_transferencia_invalida_quando_valor_superior_100000()
        {
            Transferencia transferencia = new Transferencia(1, _dados, _dados, 100_001);

            Assert.Equal(Status.ERRO, transferencia.Status);
        }

        [Fact]
        public void Criar_transferencia_CC_quando_for_mesmo_banco()
        {
            DadosTransacao beneficiario = new DadosTransacao("", "Santander", "1", "1");

            Transferencia transferencia = new Transferencia(1, _dados, beneficiario, 600);

            Assert.Equal(Tipo.DOC, transferencia.Tipo);
        }

        [Fact]
        public void Criar_transferencia_DOC_quando_valor_acima_5000_e_banco_diferente()
        {
            Transferencia transferencia = new Transferencia(1, _dados, _dados, 100);

            Assert.Equal(Tipo.CC, transferencia.Tipo);
        }
    }
}
