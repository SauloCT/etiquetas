


using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoCheque : EntityLastUpdate
    {
        public DtoCheque()
        {
        }


        public string Cliente { get; set; }
        public string ClienteID { get; set; }


        public string Empresa { get; set; }
        public string EmpresaID { get; set; }

        [AutoIncrement]

        public long Codigo { get; set; }


        public IEnumerable<Lancamentos> Lancamentos { get; set; }

        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }


        public double Valor { get; set; }


        public StatusCheque Status { get; set; }

        public string Banco { get; set; }
        public string NumeroBanco { get; set; }
        public int Agencia { get; set; }
        public int DigitoAgencia { get; set; }
        public long Conta { get; set; }
        public int DigitoConta { get; set; }


        public long NumeroCheque { get; set; }
    }
    public class Lancamentos
    {
        public string LancamentoID { get; set; }
        public string EmpresaID { get; set; }
        public string Empresa { get; set; }
        public string ClienteID { get; set; }
        public string Cliente { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataFluxo { get; set; }
        public bool Despesa { get; set; }
        public long CodigoSequencial { get; set; }
        public double ValorEntrada { get; set; }
        public double ValorSaida { get; set; }
        public double SaldoAtual { get; set; }
        public double Desconto { get; set; }
        public double Recebido { get; set; }
        public double Multa { get; set; }
        public double Juro { get; set; }
        public double ValorPago { get; set; }
        public double Valor { get; set; }
        public double ValorQuitar { get; set; }
        public double ValorPagamento { get; set; }
    }

    public enum StatusCheque
    {
        ACompensar,
        Pago,
        Devolvido
    }

    public class Cheques
    {
        public string ChequeID { get; set; }
        public string Empresa { get; set; }
        public string Status { get; set; }
        public int Lancamentos { get; set; }
        public double Valor { get; set; }
        public long Codigo { get; set; }
        public string DataPagamento { get; set; }
        public string DataVencimento { get; set; }
    }
}