using VendaERP.Core;

namespace App.VendaERP.Core.Models
{
    public class DtoEtiquetasPadroes : Entity
    {
        public string? Nome;
        public TipoPapel? Papel;
        public double? AlturaPapel;
        public double? LarguraPapel;
        public double? EspacamentoHorizontal;
        public double? EspacamentoVertical;
        public double? Largura;
        public double? Altura;
        public double? MargemEsquerda;
        public double? MargemSuperior;
        public int? Colunas;
        public int? Linhas;
        public double? ZoomImpressao;
        public bool? PadraoSistema;
        public int? TamanhoFonte;
        public int? TamanhoPreco;
        public double? AlturaEAN;
    }

    public enum TipoPapel
    {
        A4 = 0,
        Letter = 1, 
        Outro = 2
    }
}
