using System;




namespace VendaERP.Core.Models
{
    public class DtoEstoqueProdutoStatus : EntityLastUpdate
    {

        public string Status { get; set; }


        public bool PermitirVendas { get; set; }
    }
}