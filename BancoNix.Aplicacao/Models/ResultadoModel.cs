using System.Collections.Generic;

namespace BancoNix.Aplicacao.Models
{
    public class ResultadoTransferenciaModel
    {
        public double Somatoria { get; set; }
        public int Quantidade { get; set; }
        public List<TransferenciaModel> Transferencias { get; set; }
    }
}
