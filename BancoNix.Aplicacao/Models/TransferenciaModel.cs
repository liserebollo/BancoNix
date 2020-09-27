using BancoNix.Dominio;
using BancoNix.Dominio.Enums;
using System;

namespace BancoNix.Aplicacao.Models
{
    public class TransferenciaModel
    {
        public int Id { get; set; }
        public UsuarioModel Usuario { get; set; }
        public DadosTransacaoModel Beneficiario { get; set; }
        public DadosTransacaoModel Pagador { get; set; }
        public double Valor { get; set; }
        public Tipo Tipo { get; set; }
        public Status Status { get; set; }
        public DateTime Data { get; set; }
    }
}
