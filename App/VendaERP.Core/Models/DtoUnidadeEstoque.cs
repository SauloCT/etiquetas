using System;




namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueUnidade : Entity
    {

        public string Codigo { get; set; }


        public string Nome { get; set; }
    }
}