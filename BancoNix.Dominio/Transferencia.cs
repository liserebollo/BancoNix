using BancoNix.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BancoNix.Dominio
{
    public class Transferencia
    {
        private const int valorMaximoTransferencia = 100000;
        private const int valorMaximoTed = 5000;
        private readonly TimeSpan horaMaximaTed = TimeSpan.FromHours(16);
        private readonly TimeSpan horaMinimaTed = TimeSpan.FromHours(10);

        protected Transferencia()
        {

        }

        public Transferencia(int usuarioId, DadosTransacao dadosPagador, DadosTransacao dadosBeneficiario, double valor)
        {
            var dataTransferencia = DateTime.Now;

            UsuarioId = usuarioId;
            Beneficiario = dadosBeneficiario;
            Pagador = dadosPagador;
            Valor = valor;

            if ((dadosBeneficiario.Banco?.Equals(dadosPagador.Banco, StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault())
            {
                Tipo = Tipo.CC;
            }
            else if (valor < valorMaximoTed && dataTransferencia.TimeOfDay >= horaMinimaTed && dataTransferencia.TimeOfDay <= horaMaximaTed)
            {
                Tipo = Tipo.TED;
            }
            else
            {
                Tipo = Tipo.DOC;
            }

            Data = dataTransferencia;
            Status = valor > valorMaximoTransferencia ? Status.ERRO : Status.OK;
        }

        public int Id { get; set; }
        public Usuario Usuario { get; set; }

        public int UsuarioId { get; set; }
        public DadosTransacao Beneficiario { get; set; }
        public DadosTransacao Pagador { get; set; }
        public double Valor { get; set; }
        public Tipo Tipo { get; set; }
        public Status Status { get; set; }
        public DateTime Data { get; set; }
        public bool Removida { get; set; }
    }
}
