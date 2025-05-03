using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoEtiqueta
    {
        public int Codigo { get; set; }
        public int CodigoSistema { get; set; }
        public string EmpresaID { get; set; }
        public string RazaoSocial { get; set; }
        public string DadosCabecalho { get; set; }
        public string DadosRemetente { get; set; }
        public string DadosDestinatario { get; set; }
        public string Cliente { get; set; }
        public string CodigoProduto { get; set; }
        public string Produto { get; set; }
        public string EAN { get; set; }
        public string Marca { get; set; }
        public string Unidade { get; set; }
        public string NotaFiscal { get; set; }
        public string Volume { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public int BigBag { get; set; }
        public string Lote { get; set; }
        public string NumeroSerie { get; set; }
        
    }
}
