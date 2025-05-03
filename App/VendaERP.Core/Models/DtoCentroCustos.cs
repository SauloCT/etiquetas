


using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoCentroCustos : Entity
    {

        public string Cod { get; set; }



        public string Nome { get; set; }



        public string Hierarquia { get; set; }

        public string CentroPaiId { get; set; }

        public int OrdemNivel { get; set; }

        /*[ColumnProperties("left", "Data Inclusão/Modificação", Format = "{0:dd/MM/yyyy}", ColumnVisibility = true, PivotVisibility = true)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataAlteracao { get; set; }*/
    }
}