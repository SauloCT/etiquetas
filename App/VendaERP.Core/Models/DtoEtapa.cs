using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEtapa : Entity
    {
        public string Produto { get; set; }

        public string ProcessoProdutivo { get; set; }

        public TimeSpan TempoProducao { get; set; }

        public string TempoProducaoTipo { get; set; }

        public int QuantidadeProduzida { get; set; }

        public int ProducaoMinina { get; set; }

        public TimeSpan TempoDeInicializacao { get; set; }

        public string TempoDeInicializacaoTipo { get; set; }

        public TimeSpan TempoDeFinalizacao { get; set; }

        public string TempoDeFinalizacaoTipo { get; set; }

        public string Etapa { get; set; }

        public int Ordem { get; set; }

    }
}