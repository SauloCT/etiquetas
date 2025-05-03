using System;
using System.Collections.Generic;


using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLogOFX : Entity
    {
        public DtoLogOFX()
        {
            this.LancamentosCriados = new List<LancamentoOFX>();
            this.LancamentosVinculado = new List<LancamentoOFX>();
        }

        public string Tipo { get; set; }

        public string Username { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        public string FileString { get; set; }

        public ArquivoOFX ArquivoDto { get; set; }

        public List<LancamentoOFX> LancamentosVinculado { get; set; }

        public List<LancamentoOFX> LancamentosCriados { get; set; }

        public class LancamentoOFX
        {
            public string LancamentoID { get; set; }

            public string OFXKeyID { get; set; }

            public long CodigoSequencial { get; set; }
        }
    }

    public class ArquivoOFX
    {
        public ArquivoOFX()
        {
            Transacoes = new List<Transacao>();
        }

        public string CodigoBanco { get; set; }
        public string NumeroConta { get; set; }
        public string NumeroAgencia { get; set; }
        public string BancoID { get; set; }
        public string BancoNome { get; set; }
        public string EmpresaID { get; set; }
        public string EmpresaNome { get; set; }

        public DateTime DataServer { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public bool? FiltroPagamentosVinculados { get; set; }

        public List<Transacao> Transacoes { get; set; }
    }

    [Serializable]
    public class Transacao
    {
        public Guid TempId { get; set; }
        public bool Despesa { get; set; }
        public DateTime Data { get; set; }
        public double ValorTotal { get; set; }
        public string Documento { get; set; }
        public string TransacaoID { get; set; }
        public string Descricao { get; set; }
        public bool HasOnlyPagamentosVinculados { get; set; }
        public bool HasVariationValue { get; set; }
        public List<TransacaoLancamento> Lancamentos { get; set; }

        public Transacao()
        {
            Lancamentos = new List<TransacaoLancamento>();
        }
    }

    public class TransacaoLancamento
    {
        public Guid TempId { get; set; }
        //public TipoTransacaoLancamento Tipo { get; set; }
        public bool Valido { get; set; }

        public string LancamentoId { get; set; }
        public string PagamentoId { get; set; }
        public int Codigo { get; set; }
        public string PlanoDeConta { get; set; }
        public string PlanoDeContaId { get; set; }
        public string ClienteFornecedor { get; set; }
        public string ClienteFornecedorId { get; set; }
        public double Valor { get; set; }
    }
}
