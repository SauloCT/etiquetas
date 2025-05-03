using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using BorlanV2.DAO.DTO.SIGE_Entidades;

namespace VendaERP.Core.Models
{
    public class DtoProduto : EntityLastUpdate
    {
        // Construtor para normalizar dados necessarios
        public DtoProduto()
        {
            IPI_SituacaoTributaria = string.Empty;
            TipoPrecoFixo = ePrecoFixo.MVA_Porcentagem;
            

            if (ProdutoCaracteristicas == null)
            {
                ProdutoCaracteristicas = new List<CaracteristicaValor>();
            }

            if (ProdutoOpcoesAlimenticio == null) ProdutoOpcoesAlimenticio = new List<ProdutoOpcoesAlimenticio>();
        }

        [IndexedColumn()]
        public string CategoriaID { get; set; }

        public string Categoria { get; set; }

        public string MarcaID { get; set; }


        public string Marca { get; set; }

        public string FornecedorID { get; set; }


        public string Fornecedor { get; set; }

        [IndexedColumn()]

        public double Codigo { get; set; }

        // Codigo Nota Fiscal que Aceita Numeros
        [IndexedColumn()]

        public string CodigoNFe { get; set; }

        // Codigo para referência com importação do Mercado Livre
        [IndexedColumn()]
        public string CodigoML { get; set; }

        // Codigo Anterior - SPED
        public string CodigoNFe_Anterior { get; set; }

        [Obsolete("Os números de Série do Produto devem ficar relacionado ao Produto no Depósito, e não somente ao produto!")]
        public string NumeroSerie { get; set; }

        [IndexedColumn()]
        [KeyAttribute("Produto")]

        public string Nome { get; set; }

        public string GrupoTributarioID { get; set; }

        public string GrupoTributario { get; set; }

        public string CodigoGenero { get; set; }

        public string Genero { get; set; }

        public string EstoqueUnidadeID { get; set; }

        public string EstoqueUnidade { get; set; }

        public string EstoqueUnidadeCodigo { get; set; }

        public string Especie { get; set; }

        public string Prateleira { get; set; }
        public int LimiteQuantidadeVenda { get; set; }

        public string Especificacao { get; set; }

        [BsonIgnore]
        public string SaldosEstoque { get; set; }

        public double EstoqueSaldo { get; set; }

        public int LinhaProducao { get; set; }

        public double EstoqueRisco { get; set; }

        public double EstoqueCompra { get; set; }

        public bool Composto { get; set; }

        public double PesoKG { get; set; }

        public string TamanhoID { get; set; }

        public string Tamanho { get; set; }

        public string GradeID { get; set; }

        public string Grade { get; set; }

        public double PrecoCusto { get; set; }

        public double LucroPercentual { get; set; }

        public double LucroDinheiro { get; set; }

        [Obsolete("Utilizar propriedade TipoPrecoFixo")]
        public bool LucroFixo { get; set; }


        public double PrecoVenda { get; set; }

        public double ValorMinimo { get; set; }

        public double FreteCompraPercentual { get; set; }

        public double SeguroCompraPercentual { get; set; }

        public double ICMSCompraPercentual { get; set; }

        public double IPICompraPercentual { get; set; }

        public double Despesas { get; set; }

        public double AcrescimosProduto { get { return FreteCompraPercentual + SeguroCompraPercentual + ICMSCompraPercentual + IPICompraPercentual + Despesas; } set { } }

        public double Comissao { get; set; }

        public double ComissaoDinheiro { get; set; }

        public bool Promocao { get; set; }

        public double PromocaoValor { get; set; }

        public DateTime PromocaoFim { get; set; }

        public bool VisivelSite { get; set; }

        public string NomeWeb { get; set; }

        public bool DestaqueSite { get; set; }

        public bool EstoqueNegativo { get; set; }

        public string EAN_NFe { get; set; }

        public string EAN_UnidadeTributavel_NFe { get; set; }

        public string IPI_SituacaoTributaria { get; set; }

        public double IPI { get; set; }

        public string IPI_ClasseEnquadramento { get; set; }

        public string IPI_EnquadramentoLegal { get; set; }

        public string NCM_NFe { get; set; }

        public string DescricaoNCM_NFe { get; set; }

        public string NCM_EX_IPI { get; set; }

        public string CEST_NFe { get; set; }

        public string UnidadeTributavel_NFe { get; set; }

        public string QuantidadeTributavel_NFe { get; set; }

        public bool ProdutoPossuiUcomDiferenteDeUtrib { get; set; }

        public double FatorDeConversaoUnidadeComercialParaTributavel { get; set; }

        public string UnidadeComercial_NFe { get; set; }

        public string ModalidadeBC_ICMSST_NFe { get; set; }

        public int OrigemMercadoria_NFe { get; set; }

        public string CFOPPadrao_NFeID { get; set; }

        public string CFOPPadrao_NFe { get; set; }

        public bool ProduzidoEscalaNaoRelevante { get; set; }

        public string Fabricante { get; set; }

        public string FabricanteID { get; set; }

        public string CodigoBeneficioFiscal { get; set; }

        public bool UnidadePossuiNumeroSerie { get; set; }

        [Obsolete("Usar método da BlProdutosDepositos")]
        public List<string> NumerosSerieDisponiveis { get; set; }

        //Dados utilizado para fazer a composição do produto
        [BsonIgnore]
        public double QtdComposicao { get; set; }

        //Dados utilizado para fazer a composição
        [BsonIgnore]
        public string ProdutoPaiId { get; set; }

        [BsonIgnore]
        public string KitProdutoID { get; set; }

        //Dados utilizado para fazer a composição do produto
        [BsonIgnore]
        public string Etapa { get; set; }

        //Dados utilizado para fazer a impressão de etiquetas
        [BsonIgnore]
        public int ReproducaoEtiquetas { get; set; }

        // dados de ecommerce
        public string FiltrosCategoria { get; set; }

        public double PercentualDescontoBoleto { get; set; }

        public int MaximoParcelasCartao { get; set; }

        public bool FreteGratis { get; set; }

        // representação
        public bool EhRepresentacao { get; set; }

        public double ComissaoRepresentacao { get; set; }

        public string GeneroFiscal { get; set; }

        public string TecnicoID { get; set; }

        public string Tecnico { get; set; }

        public bool IgnorarEstoque { get; set; }

        public string TipoServicoID { get; set; }

        public string TipoServico { get; set; }

        public string ServicoCodigo { get; set; }

        // NFe
        public double PercentualCargaTributariaMedia { get; set; }

        //novos
        public string CodigoFornecedor { get; set; }

        public bool CalcularCustoComposicao { get; set; }

        public bool Ativo { get; set; }


        public bool OcultarNasVendas { get; set; }

        [BsonIgnore]
        public bool ExibirAbaEcommerce { get; set; }

        #region Magento


        public int MagentoProductId { get; set; }

        public bool MagentoNeedUpdate { get; set; }

        #endregion

        #region MercadoLivre

        public string MercadoLivreProductId { get; set; }

        #endregion



        [BsonIgnore]
        public double STAliquota { get; set; }

        [BsonIgnore]
        public bool PossuiLotes { get; set; }

        public DtoAtributosProduto[] Atributos { get; set; }

        public bool EhServico()
        {
            if (string.IsNullOrEmpty(Genero))
            {
                return false;
            }

            return Genero.Contains("Serviço");
        }

        public bool PossuiInformacoesSTRetidosAnteriormente { get; set; }
        public double Aliquota_Suportada_ConsumidorFinal { get; set; }
        public double FCPST_BaseCalculo { get; set; }
        public double FCPST_Percentual { get; set; }
        public double FCPST_Valor { get; set; }
        public double Valor_ICMS_Substituto { get; set; }

        public bool OcultarCampos { get; set; }

        public bool ComposicaoKit { get; set; }

        public bool ProdutoControladoPorLote { get; set; }

		public bool PossuiRastreabilidadeSEFAZ { get; set; }

        public bool ProdutoPossuiInfComb { get; set; }

        public string CodigoANP { get; set; }

        public string DescricaoANP { get; set; }

        public double PercentualGLP { get; set; }

        public double PercentualGLPNacional { get; set; }

        public double PercentualGLPImportado { get; set; }

        /// <summary>
        /// Deve ser informado nesse campo o valor por quilograma sem ICMS (Conforme Norma Técnica 2016 002)
        /// </summary>
        public double ValorPartidaGLP { get; set; }

        public bool EhArmadeFogo { get; set; }

        public int ArmaTipo { get; set; }

        public string ArmaDescricao { get; set; }

        public ePrecoFixo TipoPrecoFixo { get; set; }


        public TipoProduto Tipo { get; set; }

        public bool? IsAlimenticio { get; set; }

        public int QuantidadeMaximaSabores { get; set; }

        public string ProdutoVariacaoPaiID { get; set; }

        public List<CaracteristicaValor> ProdutoCaracteristicas { get; set; }

        public List<ProdutoOpcoesAlimenticio> ProdutoOpcoesAlimenticio { get; set; }

        

        public double? Isr { get; set; }

        public double? Iva { get; set; }

        public double? Ivaret { get; set; }

        public double? Ieps { get; set; }

        public string PlanoDeContaID { get; set; }

        public string PlanoDeConta { get; set; }


        public List<DtoProdutoSimilar> ProdutosSimilares { get; set; }

        public bool? Food { get; set; }

        public int MaxQuantityVariation { get; set; }

        public List<ProductFoodOptions> FoodOptions { get; set; }

        public string CategoryId { get; set; }

        [BsonIgnore]
        public int ProductGroupId { get; set; }

        public double TaxaMontagem { get; set; }

        public double TaxaMontagemDinheiro { get; set; }

    }

    [Serializable]
    public enum TipoProduto
    {
        Simples,
        Variavel,
        Variacao,
        Padrao,
        Pizza
    }

    [Serializable]
    public enum ePrecoFixo
    {
        Antigo = 0,
        MVA_Dinheiro = 1,
        MVA_Porcentagem = 2,
        Preco_Venda = 3
    }

    public class ProductFoodOptions
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool Pause { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public short Quantity { get; set; }

        public double SalePrice { get; set; }

        public double SaleTotalPrice { get; set; }

        public double SaleDiscount { get; set; }

        public double SaleAddition { get; set; }
    }

    public enum ProductFoodGroupSelectionType
    {
        Single,
        Multiple
    }

    [Serializable]
    public class ProdutoOpcoesAlimenticio
    {
        //sempre igual a DtoProdutoOpcoesAlimenticio

        public string Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool Pause { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public ProductFoodOptionType Type { get; set; }

        public ProductFoodGroup Group { get; set; }

        public List<ProductFoodOptionsPrice> Prices { get; set; }

        public short Quantity { get; set; }

        public double SalePrice { get; set; }

        public double SaleTotalPrice { get; set; }

        public double SaleDiscount { get; set; }

        public double SaleAddition { get; set; }
    }
}