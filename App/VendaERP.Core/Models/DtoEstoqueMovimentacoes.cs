using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoEstoqueMovimentacao : Entity
    {
        public string MovimentacaoID { get; set; }

        public string ProdutoUnidade { get; set; }

        public string ProdutoID { get; set; }

        public string DepositoID { get; set; }


        public string Tipo { get; set; }        


        public double Quantidade { get; set; }


        public DateTime Data { get; set; }


        public string Movimentacao { get; set; }

        public double ProdutoCodigo { get; set; }
        

        public string ProdutoCodigoNFE { get; set; }


        public string Produto { get; set; }


        public string Deposito { get; set; }


        public string ClienteFornecedor { get; set; }

        

        public string NotaFiscal { get; set; }


        public double ValorUnitario { get; set; }


        public double ValorTotal { get; set; }


        public string Observacoes { get; set; }

        public string Anotacao { get; set; }

        public bool SemLote { get; set; }


        public string Lote { get; set; }


        public DateTime Validade { get; set; }


		public DateTime Fabricacao { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public string VendaID { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }
    }
}