namespace BancoNix.Aplicacao.Models
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {

        }

        public UsuarioModel(int id, string nome, string cnpj)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
}
