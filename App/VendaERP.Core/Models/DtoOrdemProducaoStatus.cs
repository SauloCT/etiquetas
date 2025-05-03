

using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemProducaoStatus : Entity
    {
       
        public string Nome { get; set; }


        public string Observacoes { get; set; }
    }
}