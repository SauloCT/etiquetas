


using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoVenda : EntityLastUpdate
    {
        public DtoVenda()
        {
            
        }

        public string DepositoID { get; set; }

        public string Deposito { get; set; }

        [AutoIncrement]
        [IndexedColumn()]

        public long Codigo { get; set; }

        [IndexedColumn()]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime Data { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataAprovacaoOrcamento { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [IndexedColumn()]
        public DateTime DataAprovacaoPedido { get; set; }

        public int OportunidadeCodigo { get; set; }

        [IndexedColumn()]

        public string Status { get; set; }

        public string StatusClienteID { get; set; }

        public string StatusCliente { get; set; }

        public string VendaCategoriaID { get; set; }

        public string VendaCategoria { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Alteracao { get; set; }

        public string AlteradoPor { get; set; }

        public bool StatusAlterado { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Validade { get; set; }

        // FK Empresa
        [IndexedColumn()]
        public string EmpresaId { get; set; }

        public string Empresa { get; set; }

        // FK Cliente
        [IndexedColumn()]
        public string ClienteId { get; set; }

        [IndexedColumn()]
        public string Cliente { get; set; }

        public string EmailResponsavel { get; set; }

        public string EmailFaturamento { get; set; }

        public string EmailComercial { get; set; }

        public string RepresentadaID { get; set; }

        public string Representada { get; set; }


        /// <summary>
        /// Esta propridade refere-se ao ID do usuario no ASPNETUsers
        /// <para></para>
        /// NÃO É O ID DA PESSOA, CHAMAR O METODO Get_IdPessoa_ForASPNETUser da BlUsuariosSistema para obter o Id da pessoa vinculada a este usuario
        /// </summary>
        public string VendedorID { get; set; }

        public string VendedorPessoaID { get; set; }

        public string Vendedor { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataEnvio { get; set; }

        public double Desconto { get; set; }

        public string Descricao { get; set; }

        public string TipoComprovante { get; set; }

        public bool FreteContaEmitente { get; set; }

        public double OutrasDespesas { get; set; }

        public double DescontoDinheiro { get; set; }

        public double DescontoPercentual { get; set; }

        public double ValorImpostos { get; set; }

        public double ValorComissao { get; set; }

        public double ValorSemImpostos { get; set; }

        public double ValorFinal { get; set; }

        public bool Finalizado { get; set; }

        public bool Lancado { get; set; }

        public bool FreteLancado { get; set; }

        public bool Enviado { get; set; }

        public string MotivoCancelamento { get; set; }

        [BsonIgnore]
        public double ComissoesAReceber { get; set; }

        [BsonIgnore]
        public double ComissoesRecebidas { get; set; }

        // representacao
        [IndexedColumn()]
        public string OrigemVenda { get; set; }

        public string Plataforma { get; set; }

        public string FormadePagamentoID { get; set; }

        public string FormadePagamento { get; set; }

        public CondicaoPagamento CondicaoPagamento { get; set; }

        public int NumeroParcelasvenda { get; set; }

        public IntervaloParcelamento TipoIntervaloParcelas { get; set; }

        public int PeriodoParcelas { get; set; }

        public double Adiantamento { get; set; }

        public double ValorComissaoRepresentacao { get; set; }

        public bool ComissaoVendedorLancada { get; set; }

        // NFe Gerada apartir desta venda
        public string NumeroNFe { get; set; }

        public string Condicoes { get; set; }

        public string PlanoDeContaID { get; set; }

        public string PlanoDeConta { get; set; }

        public string CodigoPedidoCliente { get; set; }

        #region Ordem de Serviço
        public bool PossuiOrdemServico { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime AberturaOrdemServico { get; set; }

        public string TecnicoID { get; set; }

        public string Tecnico { get; set; }

        public string Contrato { get; set; }

        public string Contato { get; set; }

        public string Problema { get; set; }

        public string Laudo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataVisita { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime IntervencaoTecnicaInicio { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime IntervencaoTecnicaFim { get; set; }

        public bool OrdemServicoConcluida { get; set; }

        public bool OrdemServicoCancelada { get; set; }

        public string MotivoCancelamentoOS { get; set; }
        #endregion

        // FK Tabela de Preço
        public string TabelaDePrecoId { get; set; }

        public string TabelaDePreco { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [IndexedColumn]
        public DateTime DataFaturamento { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [IndexedColumn]
        public DateTime DataFaturamentoNFE { get; set; }

        public string DetalhesFaturamento { get; set; }

        [IndexedColumn()]
        public string UsuarioID { get; set; }

        //Entrega

        public MeioEnvio FreteMeioEnvio { get; set; }

        public string Transportadora { get; set; }

        public string TransportadoraID { get; set; }

        public double ValorSeguro { get; set; }

        public string FreteFormaEnvio { get; set; }

        public FormatoEncomenda FormatoEncomenda { get; set; }

        public string CodigoRastreio { get; set; }

        public double ValorFrete { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataPostagem { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PrevisaoEntrega { get; set; }

        public string CEP { get; set; }

        public string Pais { get; set; }

        public string UF { get; set; }

        public string UFCodigo { get; set; }

        public string Municipio { get; set; }

        public string CodigoMunicipio { get; set; }

        public string Bairro { get; set; }

        public string Logradouro { get; set; }

        public string LogradouroNumero { get; set; }

        public string LogradouroComplemento { get; set; }

        public string TipoEndereco { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime? DataImportacaoViaPlanilha { get; set; }

		#region Magento

        public string APIMagentoVendaId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime APIMagentoDataVenda { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime APIMagentoUltimaSincronizacao { get; set; }

        #endregion

        #region MercadoLivre

        public long MercadoLivreVendaId { get; set; }

        #endregion

        // Cuidado também é utilizado na API do Magento
        public bool ViaAPI { get; set; }

        public bool CriadoViaAPI { get; set; }

        // Ordem de Serviço de Garantia
        public bool OSGarantia { get; set; }

        public string NFSeNumero { get; set; }


        public int PedidoRelacionado { get; set; }

        public int RevisaoNumero { get; set; }

        public bool IgnorarLimiteCredito { get; set; }

        public string PessoaID { get; set; }

        public bool EnderecoOpcional { get; set; }

        public string TransacaoCartao { get; set; }

        public int PresencaCliente { get; set; }

        // Utilizados no PDV
        [IndexedColumn("PagamentosVenda.CV_NSU")]
        public List<DtoVendaPagamento> PagamentosVenda { get; set; }

        public double QuantidadeTotalItensVendidos { get; set; }

        public double ValorTroco { get; set; }

        /************************* Dados operação cartão / Dados de TEF (Transferencia Eletronica de Fundos) ******************************/

        [Obsolete("Mudado para listagem DTOVendapagamento")]
        public string CV_NSU { get; set; } // Codigo Unico da transação

        [Obsolete("Mudado para listagem DTOVendapagamento")]
        public string NumeroTerminal { get; set; } // Codigo unico da maquina de cartão

        [Obsolete("Mudado para listagem DTOVendapagamento")]
        public string Credenciadora { get; set; }

        [Obsolete("Mudado para listagem DTOVendapagamento")]
        public string CredenciadoraCNPJ { get; set; }

        [Obsolete("Mudado para listagem DTOVendapagamento")]
        public string BandeiraCartao { get; set; }

        [Obsolete("Mudado para listagem DTOVendapagamento")]
        public DateTime DataTransacao { get; set; }

        /*********************** Dados de frete Avançado *****************************/
        public bool OpcaoFreteAvancado { get; set; }

        #region Remetente
        //Id da pessoa a ser considerada remetente da mercadoria
        public string RemetenteMercadoria_PessoaId { get; set; }

        public string RemetenteMercadoriaNome { get; set; }

        public string RemetenteMercadoriaCNPJ_CPF { get; set; }

        public bool RemetenteLocalRetiradaDifereEndereco { get; set; }

        public string RemetenteLocalRetiradaNome { get; set; }

        public string RemetenteLocalRetiradaCNPJ_CPF { get; set; }

        public string RemetenteLocalRetiradaIE { get; set; }

        public string RemetenteLocalRetiradaLogradouro { get; set; }

        public string RemetenteLocalRetiradaLogradouroNumero { get; set; }

        public string RemetenteLocalRetiradaComplemento { get; set; }

        public string RemetenteLocalRetiradaBairro { get; set; }

        public string RemetenteLocalRetiradaCEP { get; set; }

        public string RemetenteLocalRetiradaMunicipio { get; set; }

        public string RemetenteLocalRetiradaMunicipioCodigo { get; set; }

        public string RemetenteLocalRetiradaUF { get; set; }

        public string RemetenteLocalRetiradaUFCodigo { get; set; }
        #endregion

        #region Destinatario
        //Id da pessoa a ser considerada destinatario da mercadoria
        public string DestinatarioMercadoria_PessoaId { get; set; }

        public string DestinatarioMercadoriaNome { get; set; }

        public string DestinatarioMercadoriaCNPJ_CPF { get; set; }

        public bool DestinatarioLocalEntregaDifereEndereco { get; set; }

        public string DestinatarioLocalEntregaNome { get; set; }

        public string DestinatarioLocalEntregaCNPJ_CPF { get; set; }

        public string DestinatarioLocalEntregaIE { get; set; }

        public string DestinatarioLocalEntregaLogradouro { get; set; }

        public string DestinatarioLocalEntregaLogradouroNumero { get; set; }

        public string DestinatarioLocalEntregaComplemento { get; set; }

        public string DestinatarioLocalEntregaBairro { get; set; }

        public string DestinatarioLocalEntregaCEP { get; set; }

        public string DestinatarioLocalEntregaMunicipio { get; set; }

        public string DestinatarioLocalEntregaMunicipioCodigo { get; set; }

        public string DestinatarioLocalEntregaUF { get; set; }

        public string DestinatarioLocalEntregaUFCodigo { get; set; }

        //Id da pessoa a ser considerada motorista da mercadoria
        public string MotoristaMercadoria_PessoaId { get; set; }

        public string MotoristaMercadoriaNome { get; set; }

        public string MotoristaMercadoriaCNPJ_CPF { get; set; }

        public DtoVendaVeiculosTransporteMercadoria[] VeiculosTransporte { get; set; }

        public bool DestinatarioPossuiMultiplosDestinatarios { get; set; }

        public DtoVendaDestinatario_MultiplosDestinatarios[] DestinatarioMultiplosDestinatarios { get; set; }

        #endregion

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataEmailRecebido { get; set; }

        public string DataRecebimentoEmail { get; set; }

        public bool EmailRecebido { get; set; }

        public List<DtoAtributosVenda> Atributos { get; set; }

        [BsonIgnore]
        ///Auxiliar na tela de edição de vendas
        public bool EmpresaPossuiSIGEP { get; set; }

        public string AndroidVendaID { get; set; }

		public bool EcommerceEnviarMensagemNovaNFe { get; set; }
		public bool EcommerceEnviarMensagemSaindoEntrega { get; set; }
		public bool EcommerceEnviarMensagemEntregue { get; set; }
        public bool EcommerceEnviarMensagemErroEntrega { get; set; }

        public bool PossuiCodigoRastreioSIGEP { get; set; }

        public string ECommerceOrderNumber { get; set; }

        public string NumeroDAV { get; set; }

        public bool PreFaturadoECommerce { get; set; }

        public string CpfCnpjConsumidorNaoIdentificado { get; set; }

        public string CondicaoPagamentoPersonalizado { get; set; }

        //Lucratividade
        public double CustoTotalDaVenda { get; set; }

        public string PeriodoRecebimento { get; set; }

        public List<ProductGroup> GruposProdutos { get; set; }

        public double ValorTaxaMontagem { get; set; }

       
        public string StatusEnvio { get; set; }
        public string MensagemStatusEnvio { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime? DataEntregaDespacho { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime? DataErroEntrega { get; set; }

        public string Channel { get; set; } // Propriedade para uso exclusivo do B2W
    }

    [Serializable]
    public class DtoVendaVeiculosTransporteMercadoria
    {
        public string Tipo { get; set; }
        public string Identificacao { get; set; }

    }

    //Que nome ruim para esta classe, mas foi o melhor que achei
    // Realmente, é um nome ruim
    [Serializable]
    public class DtoVendaDestinatario_MultiplosDestinatarios
    {
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public string PessoaId { get; set; }
        public string Observacoes { get; set; }
    }

    [Serializable]
    public class DtoVendaPagamento : Entity
    {
        public DtoVendaPagamento()
        {
            this.LancamentosIDs = new List<string>();
        }

        public string FormaPagamento { get; set; }
        public string DescricaoPagamento { get; set; }
        public double ValorPagamento { get; set; }
        public double ValorDesconto { get; set; }
        public string BandeiraCartao { get; set; }
        public bool pago { get; set; }
        public string BandeiraCartaoTextString
        {
            get
            {
                switch (this.BandeiraCartao)
                {
                    case "01":
                        return "Visa";
                    case "02":
                        return "Mastercard";
                    case "03":
                        return "American Express";
                    case "04":
                        return "Sorocred";
                    case "05":
                        return "Diners Club";
                    case "06":
                        return "Elo";
                    case "07":
                        return "Hipercard";
                    case "08":
                        return "Aura";
                    case "09":
                        return "Cabal";
                    case "99":
                        return "Outros";
                    default:
                        return this.BandeiraCartao;
                }
            }
        }

        public string IndicadorFormaPagamento { get; set; }
        public double NumeroParcela { get; set; }
        public string NumeroTerminal { get; set; }
        public DateTime DataTransacao { get; set; }

        public string CredenciadoraCartao { get; set; }
        public string CredenciadoraCNPJ { get; set; }
        public string CV_NSU { get; set; }
        public string TipoIntegracao { get; set; }

        public CondicaoPagamento CondicaoPagamento { get; set; }
        public int Parcelas { get; set; }
        public int PeriodoParcelas { get; set; }
        public int PeriodoParcelasOutros { get; set; }
        public double Adiantamento { get; set; }
        public string FormaPagamentoID { get; set; }
        public int codigo { get; set; }
        public List<string> LancamentosIDs { get; set; }
        public DateTime dataVencimento { get; set; }
        public bool ehAdiantamento { get; set; }
        public string NumeroDuplicata { get; set; }

    }

    public enum MeioEnvio
    {
        Correios = 1,
        Outros = 0
    }

    public enum FormaEnvioCorreio
    {
        PAC = 41106,
        SEDEX = 40010,
        SEDEXACOBRAR = 40045,
        SEDEX10 = 40215,
        SEDEXHOJE = 40290
    }

    public enum FormatoEncomenda
    {
        CAIXAPACOTE = 1,
        ROLOPRISMA = 2
    }

    public enum CondicaoPagamento
    {
        AVista = 0,
        APrazo = 1,
    }

    public static class OrigemVendaStr
    {
        public const string PDV = "PDV";
        public const string VendaDireta = "Venda Direta";
        public const string ECommerce = "E-commerce";
        public const string iFood = "iFood";
    }

    public static class StatusVenda
    {
        /// <summary>
        /// Pedido
        /// </summary>
        public const string Pedido = "Pedido";
        /// <summary>
        /// Orçamento
        /// </summary>
        public const string Orcamento = "Orçamento";
        /// <summary>
        /// Pedido Faturado
        /// </summary>
        public const string PedidoFaturado = "Pedido Faturado";
        /// <summary>
        /// Pedido Cancelado
        /// </summary>
        public const string PedidoCancelado = "Pedido Cancelado";
        /// <summary>
        /// Pedido Aprovado Sem Faturamento
        /// </summary>
        public const string PedidoAprovadoSemFaturamento = "Pedido Aprovado Sem Faturamento";
        /// <summary>
        /// Pedido Nao Faturado
        /// </summary>
        public const string PedidoNaoFaturado = "Pedido Nao Faturado";
        /// <summary>
        /// Pedido Pré-Faturado
        /// </summary>
        public const string PedidoPreFaturado = "Pedido Pré-Faturado";
    }

    public class ProductGroup
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
