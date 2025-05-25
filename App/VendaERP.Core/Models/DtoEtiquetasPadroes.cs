using VendaERP.Core;

namespace App.VendaERP.Core.Models
{
    public class DtoEtiquetasPadroes : Entity
    {
        public string? Nome { get; set; }
        public TipoPapel? Papel { get; set; }
        public double? AlturaPapel { get; set; }
        public double? LarguraPapel { get; set; }
        public double? EspacamentoHorizontal { get; set; }
        public double? EspacamentoVertical { get; set; }
        public double? Largura { get; set; }
        public double? Altura { get; set; }
        public double? MargemEsquerda { get; set; }
        public double? MargemSuperior { get; set; }
        public int? Colunas { get; set; }
        public int? Linhas { get; set; }
        public double? ZoomImpressao { get; set; }
        public bool? PadraoSistema { get; set; }
        public int? TamanhoFonte { get; set; }
        public int? TamanhoPreco { get; set; }
        public double? AlturaEAN { get; set; }
    }

    public enum TipoPapel
    {
        A4 = 0,
        Letter = 1, 
        Outro = 2
    }
}
