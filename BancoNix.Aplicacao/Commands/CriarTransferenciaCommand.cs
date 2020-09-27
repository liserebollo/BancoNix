using BancoNix.Aplicacao.Models;

namespace BancoNix.Aplicacao.Commands
{
    public class CriarTransferenciaCommand
    {
        public int UsuarioId { get; set; }
        public DadosTransacaoModel Beneficiario { get; set; }
        public DadosTransacaoModel Pagador { get; set; }
        public double Valor { get; set; }
    }
}
