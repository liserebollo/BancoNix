using System;
using System.Collections.Generic;
using System.Text;

namespace BancoNix.Dominio
{
    public class Usuario
    {
        public Usuario(int id, string nome, string cnpj)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
        }

        protected Usuario()
        {

        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        public List<Transferencia> Transferencias { get; set; }
    }
}
