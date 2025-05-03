using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoContrato : Entity
    {
       

        public double Codigo { get; set; }

        public string EmpresaID { get; set; }


        public string Empresa { get; set; }

        public string TipoContratoID { get; set; }

      

        public string TipoContrato { get; set; }

        public string ClienteID { get; set; }

        

        public string Cliente { get; set; }

        

        public int DiaVencimento { get; set; }

        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime Inicio { get; set; }

        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime Termino { get; set; }

       
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime UltimoReajuste { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime ProximoReajuste { get; set; }

       

        public string Situacao { get; set; }

        public bool SincronizarFinanceiro { get; set; }

        public string PlanoDeContaID { get; set; }

        public string PlanoDeConta { get; set; }

        public string CentroDeCustoID { get; set; }

        public string CentroDeCusto { get; set; }

        public string FormaDePagamentoID { get; set; }

       

        public string FormaDePagamento { get; set; }

        public string BancoID { get; set; }

        public string Banco { get; set; }

        public int Duracao { get; set; }

        public int DiasCarencia { get; set; }

       

        public double Valor { get; set; }

        public string Observacoes { get; set; }

        //deixei salvar no banco pra não dar problema com os contratos já salvos
        public string PeriodoDuracao { get; set; }

        public int NumeroParcelasContrato { get; set; }

        public int PeriodoParcelas { get; set; }

        public IntervaloParcelamento Intervalo { get; set; }

        public int DiasIntervalo { get; set; }

        public double Adiantamento { get; set; }

        public string VendedorID { get; set; }

        public string VendedorPessoaID { get; set; }

        public string Vendedor { get; set; }

        public double ComissaoVendedor { get; set; }

        public bool ComissaoVendedorLancada { get; set; }

        [Obsolete("Um contrato pode ter mais que uma NFSe")]
        public string NFSeNumero { get; set; }

        public bool ValorTotalChanged { get; set; }
        

        [BsonIgnore]
        public bool VincularComissaoLancamentos { get; set; }
    }

    public static class PeriodoDuracaoContratoEnum
    {
        public const string Dias = "0";
        public const string Semanas = "1";
        public const string Meses = "2";
    }
}