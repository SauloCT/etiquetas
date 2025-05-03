using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoProdutoFaturamentoParcial : EntityLastUpdate
    {
        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        public string VendaID { get; set; }

        public string OSID { get; set; }

        public string ProdutoID { get; set; }

        public double Codigo { get; set; }

        public string CodigoNFE { get; set; }

        public string Unidade { get; set; }

        public string Descricao { get; set; }

        public string Marca { get; set; }

        public double Quantidade { get; set; }

        public double QuantidadeRateio { get; set; }

        public double QuantidadeComposicaoOriginal { get; set; }

        public double ValorUnitario { get; set; }

        public double ValorUnitarioSemComissao { get; set; }

        public double ComissaoUnitario { get; set; }

        public double ComissaoUnitarioPercentual { get; set; }

        public double ValorTotal { get; set; }

        public double ValorCustoUnitario { get; set; }

        public double AcrescimosCustoProduto { get; set; }

        public double ValorCustoTotal
        {
            get
            {
                return this.Quantidade * this.ValorCustoUnitario;
            }
            set
            {

            }
        }

        public double MVA { get; set; }

        public double ComissaoTotal { get; set; }

        public double ComissaoUnitariaRepresentacao { get; set; }

        public double ComissaoUnitariaRepresentacaoPercentual { get; set; }

        public double ComissaoTotalRepresentacao { get; set; }

        public double DescontoUnitario { get; set; }

        public double DescontoUnitarioPercentual { get; set; }

        public double DescontoTotal { get; set; }

        public bool DescontoUnitarioFixo { get; set; }

        public bool Servico { get; set; }

        #region Fiscal

        public string GrupoTributario { get; set; }

        public string DestinoPais { get; set; }

        public string DestinoEstado { get; set; }

        public string NCM { get; set; }

        public string CodigoCFOP { get; set; }

        public string TipoServico { get; set; }

        public double ValorFrete { get; set; }

        public double Seguro { get; set; }

        public double OutrasDespesas { get; set; }

        public double PercentualCargaTributariaMedia { get; set; }

        #region IPI
        public string IPI_SituacaoTributaria { get; set; }

        public string IPI_ClasseEnquadramento { get; set; }

        public string IPI_EnquadramentoLegal { get; set; }

        public double IPI_BaseCalculo { get; set; }

        public double IPI_Aliquota { get; set; }

        public double IPI_QuantidadeTotalUnidadePadrao { get; set; }

        public double IPI_ValorUnidade { get; set; }

        public double IPI_DescontoIncondicional { get; set; }

        public double IPI_Valor { get; set; }

        public bool IPI_SomarBCICMS { get; set; }

        public bool IPI_SomarBCICMS_ST { get; set; }



        #endregion

        #region ICMS
        public string ICMS_SituacaoTributaria { get; set; }

        public bool NaoOcultarICMSSTnaDANFE { get; set; }

        public string ICMS_ModalidadeDeterminacaoBC { get; set; }

        public double ICMS_ReducaoBase { get; set; }

        public double ICMS_BaseDeCalculo { get; set; }

        public double ICMS_Aliquota { get; set; }

        public double ICMS_Valor { get; set; }

        public double ICMS_DiferencialAliquota { get; set; }

        public double ICMS_DiferencialAliquotaValor { get; set; }

        public double ICMS_PercentualDiferimento { get; set; }

        public double ICMS_ValorDiferimento { get; set; }

        public double ICMS_ValorICMSDiferido { get; set; }

        public double ICMS_SimplesNacionalCredito { get; set; }

        public double ICMS_SimplesNacionalCreditoValor { get; set; }

        public double ICMS_ZFAliquota { get; set; }

        public double ICMS_ZFValor { get; set; }

        public string ICMS_STModalidadeDeterminacaoBase { get; set; }

        public double ICMS_STMVA { get; set; }

        public double ICMS_STPautaValor { get; set; }

        public double ICMS_STBaseCalculoReducao { get; set; }

        public double ICMS_STBaseCalculoValor { get; set; }

        public double ICMS_STAliquota { get; set; }

        public double ICMS_STValor { get; set; }

        public bool? ICMSST_DestacarST { get; set; }
        #endregion

        #region PIS
        public string PIS_CST { get; set; }

        public double PIS_Aliquota { get; set; }

        public double PIS_BaseCalculo { get; set; }

        public double PIS_Valor { get; set; }

        #endregion

        #region COFINS
        public string COFINS_CST { get; set; }

        public double COFINS_Aliquota { get; set; }

        public double COFINS_BaseCalculo { get; set; }

        public double COFINS_Valor { get; set; }

        #endregion

        #region Serviços - Não deve estar totalmente correto

        #region CSLL
        public double CSLLPercentualBC { get; set; }

        public double CSLLAliquota { get; set; }

        public double CSLLValorBase { get; set; }

        public double CSLLValor { get; set; }

        #endregion

        #region IRPJ

        public double IRPJPercentualBC { get; set; }

        public double IRPJAliquota { get; set; }

        public double IRPJBaseCalculo { get; set; }

        public double IRPJValor { get; set; }

        #endregion IRPJ

        #region INSS

        public double INSSAliquota { get; set; }

        public double INSSBaseCalculo { get; set; }

        public double INSSValor { get; set; }

        #endregion

        public double ISSAliquota { get; set; }

        public double ISSReducaoBaseCalculo { get; set; }

        public double ISSBaseCalculo { get; set; }

        public double ISSValor { get; set; }

        public string ItemListaServico { get; set; }

        public string CodigoTributacaoMunicipio { get; set; }

        public string CodigoNBS { get; set; }

        public bool ISSRetido { get; set; }

        public bool ISSValorRetidoAlterado { get; set; }

        public double ISSValorRetido { get; set; }

        public double ISSValorRetidoUnitario { get; set; }

        public bool NFSeEmitida { get; set; }

        #endregion

        #region ICMS UF Destino

        public double ICMS_UFDestinoBC { get; set; }

        public double ICMS_UFDestinoAliquota { get; set; }

        public double ICMS_UFDestinoValorFCPBaseCalculo { get; set; }

        public double ICMS_UFDestinoPercentualFCP { get; set; }

        public double ICMS_UFDestinoAliquotaInterEstadual { get; set; }

        public double ICMS_UFDestinoValorFCP { get; set; }

        public double ICMS_UFDestinoPercentualPartilha { get; set; }

        public double ICMS_UFDestinoValorUFDestino { get; set; }

        public double ICMS_UFDestinoValorUFRemetente { get; set; }

        #endregion

        #region ICMS Desonerado
        public bool ICMS_EhDesonerado { get; set; }

        public double ICMS_Desonerado { get; set; }

        public double ICMS_ValorDesonerado { get; set; }

        public string ICMS_MotDesICMS { get; set; }
        #endregion
        #endregion

        #region Reports
        public string Categoria { get; set; }
        #endregion

        public string Lote { get; set; }

        public bool SemLote { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

        public double PesoKG { get; set; }

        public double Comprimento { get; set; }

        public double Altura { get; set; }

        public double Largura { get; set; }

        public double Diametro { get; set; }

        public TipoObjeto TipoObjeto { get; set; }

        public bool FreteGratis { get; set; }

        public int PrazoEntregaFrete { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public int Indice { get; set; }

        public List<AtributoMovimentacao> AtributosProduto { get; set; }

        public string KitProdutoPaiID { get; set; }

        public string KitProdutoID { get; set; }

        public bool EhKit { get; set; }

        public double QuantidadeKit { get; set; }

        public List<ComposicaoProduto> Composicao { get; set; }

        public List<ProdutoSimilar> ProdutosSimilares { get; set; }

        public int ProductGroupId { get; set; }

        public bool IgnorarAlertaEstoque { get; set; }

        public double EstoqueSaldo { get; set; }

        public double EstoqueRisco { get; set; }

        public double ValorMinimo { get; set; }

        public double CompraMaxima { get; set; }

        public string OrdensProducaoCodigo { get; set; }

        public bool RecalcularTotalDesconto { get; set; }

        public double FCP_STAliquota { get; set; }

        public double FCP_ST_BaseCalculo { get; set; }

        public double FCPST_Valor { get; set; }

        public double FCP_Valor { get; set; }

        public double FCP_BaseCalculo { get; set; }

        public double FCP_Aliquota { get; set; }

        public double FCPST_Valor_Retido { get; set; }

        public double FCP_ST_BaseCalculo_Retido { get; set; }

        public double TaxaMontagem { get; set; }

        public double TaxaMontagemDinheiro { get; set; }

        public string OrdemCompraCodigo { get; set; }

        public int CodigoOrdemCompra { get; set; }

        public string SituacaoOrdem { get; set; }

        public int LimiteQuantidadeVenda { get; set; }

        public bool FaturadoParcialmente { get; set; }

        public bool NFeFaturadoParcialmente { get; set; }
    }

    public class ComposicaoProdutoFaturamentoParcial
    {
        public string ProdutoID { get; set; }

        public string Produto { get; set; }

        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        public double Quantidade { get; set; }

        public double QuantidadeTotal { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        public List<string> NumerosSerie { get; set; }
    }

    public class ProdutoSimilarFaturamentoParcial
    {
        public string ProdutoID { get; set; }

        public string Produto { get; set; }

        public string Codigo { get; set; }

        public double PrecoVenda { get; set; }

        public double SaldoEstoque { get; set; }
    }
}