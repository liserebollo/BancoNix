namespace BancoNix.Dominio
{
    public class DadosTransacao
    {
        protected DadosTransacao()
        {

        }

        public DadosTransacao(string nome, string banco, string agencia, string conta)
        {
            Nome = nome;
            Banco = banco;
            Agencia = agencia;
            Conta = conta;
        }

        public string Nome { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
    }
}
