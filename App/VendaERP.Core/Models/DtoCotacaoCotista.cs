using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;

namespace VendaERP.Core.Models
{
    public class DtoCotacaoCotista : Entity
    {
        public string CotacaoID { get; set; }

        public string FornecedorID { get; set; }

        public string FormadePagamentoID { get; set; }

        public string FormadePagamento { get; set; }

        public string Fornecedor { get; set; }

        public string Email { get; set; }

        public bool Enviado { get; set; }

        public bool Recebido { get; set; }

        public bool Respondido { get; set; }

        public bool PreenchidoUsuario { get; set; }

        public string ArquivoAnexadoID { get; set; }

        public string ArquivoAnexado { get; set; }

        public double ValorItens { get; set; }

        public double ValorFrete { get; set; }

        public double ValorOutrasDespesas { get; set; }

        [BsonIgnore]
        ///Usado somente para apresentar os Itens na Tela, não salvo no banco
        public List<DtoCotacaoItemCotado> ItensCotados { get; set; }
    }
}
