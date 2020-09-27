using BancoNix.Dominio.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace BancoNix.Aplicacao.Filtros
{
    public class FiltroTransferencia
    {
        [FromQuery(Name = "dt")]
        public DateTime? Data { get; set; }
        
        [FromQuery(Name = "np")]
        public string NomePagador { get; set; }
        
        [FromQuery(Name = "nb")]
        public string NomeBeneficiario { get; set; }

        [FromQuery(Name = "st")]
        public Status? Status { get; set; }

        [FromQuery(Name = "tp")]
        public Tipo? Tipo { get; set; }
    }
}
