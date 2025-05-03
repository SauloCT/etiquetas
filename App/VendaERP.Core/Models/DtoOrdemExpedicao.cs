using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOrdemExpedicao : Entity
    {


        [AutoIncrement]
        public int Codigo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataEmissao { get; set; }

        public string EmpresaID { get; set; }



        public string Empresa { get; set; }


        public OrdemExpedicaoStatus Status { get; set; }

        public string ResponsavelSeparacao { get; set; }

        public string ResponsavelDespacho { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataDespacho { get; set; }

        public string TransportadoraID { get; set; }


        public string Transportadora { get; set; }

        public bool VeiculoProprio { get; set; }

        public string PlacaVeiculo { get; set; }

        public string PesoTotalProdutos { get; set; }

        public string Observacoes { get; set; }

        public List<string> CodigosPedidos { get; set; }
    }

    public enum OrdemExpedicaoStatus
    {
        Aguardando = 0,
        Cancelada = 1,
        EmSeparacao = 2,
        ProntaParaDespacho = 3,
        Despachada = 4
    }
}
