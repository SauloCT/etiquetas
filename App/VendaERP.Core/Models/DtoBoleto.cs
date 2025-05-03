using System;
using System.Collections.Generic;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoBoleto : EntityLastUpdate
    {

        public long BoletoCodigo { get; set; }


        public bool IgnoreOnMaxNum { get; set; }

        public string NumeroDocumento { get; set; }

        public double ValorBoleto { get; set; }

        public string Sacado { get; set; }

        /// <summary>
        /// Aqui vai o ID do sacado da Collection DTOPessoa
        /// </summary>
        public string SacadoID { get; set; }

        /// <summary>
        /// Aqui vai o ID do banco  da Collection DTOBanco
        /// </summary>
        public string BancoID { get; set; }

        /// <summary>
        /// Aqui vai o NOME do banco da Collection DTOBanco, usado somente para mostrar na tela.
        /// </summary>
        public string Banco { get; set; }

        public string Carteira { get; set; }

        public string EspecieDocumentoCodigo { get; set; }

        public bool Pago { get; set; }

        public bool Cancelado { get; set; }
        public bool Estornado { get; set; }

        public bool RemessaEnviada { get; set; }

        public bool RetornoRecebido { get; set; }

        public string NossoNumero { get; set; }

        public string LogradouroSacado { get; set; }

        public string LogradouroNumeroSacado { get; set; }

        public string ComplementoSacado { get; set; }

        public string BairroSacado { get; set; }

        public string CidadeSacado { get; set; }

        public string CPF_CNPJSacado { get; set; }

        public string UF { get; set; }

        public string CEPSacado { get; set; }

        public string EmailSacado { get; set; }

        public string EmpresaID { get; set; }

        public string Empresa { get; set; }

        public bool ComRegistro { get; set; }

        public string Descricao { get; set; }

        public string RemessaID { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataVencimento { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataEmissao { get; set; }

        public double MultaAposVencimento { get; set; }

        public double JurosDiaAposVencimento { get; set; }

        public double DescontoAntecipacao { get; set; }

        public string Instrucoes { get; set; }

        public bool BancoEmiteBoleto { get; set; }

        public ModalidadeEmissao Modalidade { get; set; }

        public List<string> LancamentosID { get; set; }

        public string NumerosLancamentos { get; set; }

        [Obsolete("Agora o boleto pode ter mais de um lançamento")]
        public string LancamentoID { get; set; }

        [Obsolete("Agora o boleto pode ter mais de um lançamento")]
        public int NumeroLancamento { get; set; }

        public bool Protestar { get; set; }

        public int ProtestoDias { get; set; }

        public ModoContagem ModoContagemProtesto { get; set; }

        public bool Devolver { get; set; }

        public int DevolucaoDias { get; set; }

        public ModoContagem ModoContagemDevolucao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataEmailRecebido { get; set; }

        public string DataRecebimentoEmail { get; set; }

        public bool EmailRecebido { get; set; }

        public bool ImprimeComprovanteRecebimento { get; set; }

        public string TipoEndereco { get; set; }

        [BsonIgnore]
        public bool EnviarBoletoParaEmailSacado { get; set; }

        [BsonIgnore]
        public string NomeCarteira { get; set; }

        [BsonIgnore]
        public string BancoNumero { get; set; }

        [BsonIgnore]
        public bool BancoPodeEnviarBoleto { get; set; }

        [BsonIgnore]
        public bool PodeMudarModoContagemProtesto { get; set; }

        [BsonIgnore]
        public int ProtestoDiasPadrao { get; set; }

        [BsonIgnore]
        public ModoContagem ModoContagemProtestoPadrao { get; set; }

        [BsonIgnore]
        public bool PodeMudarModoContagemDevolucao { get; set; }

        [BsonIgnore]
        public int DevolucaoDiasPadrao { get; set; }

        [BsonIgnore]
        public ModoContagem ModoContagemDevolucaoPadrao { get; set; }

        public int CodigoInstrucaoRetorno { get; set; }

        public bool PagHiper { get; set; }

        public string PagHiperConfiguracaoId { get; set; }

        [IndexedColumn()]
        public string PagHiperTransactionId { get; set; }

        public string PagHiperUrlSlip { get; set; }

        public string InstrucaoAdicional { get; set; }

    }

    public enum ModoContagem
    {
        DIASCORRIDOS = 0,
        DIASUTEIS = 1
    }
}