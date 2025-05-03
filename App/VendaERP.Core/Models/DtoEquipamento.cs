


using System;
namespace VendaERP.Core.Models
{
    public class DtoEquipamento : EntityLastUpdate
    {

        public string Codigo { get; set; }


        public string Nome { get; set; }


        public string Modelo { get; set; }


        public string Fabricante { get; set; }


        public string NumeroPatrimonio { get; set; }


        public string NumeroControle { get; set; }
    }
}