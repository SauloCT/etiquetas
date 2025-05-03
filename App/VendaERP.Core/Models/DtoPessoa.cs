

using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    public class DtoPessoa : EntityLastUpdate
    {
        [IndexedColumn("NomeFantasia", "CNPJ_CPF")]
        public bool PessoaFisica { get; set; }

        [IndexedColumn()]
        [KeyAttribute("Cliente", "Fornecedor", "NomeEmpresa")]

        public string NomeFantasia { get; set; }


        public string RazaoSocial { get; set; }

        [IndexedColumn()]

        public string CNPJ_CPF { get; set; }

        [IndexedColumn()]

        public string Codigo { get; set; }

        // Inscrição Municipal
        public string IM { get; set; }

        public string RG { get; set; }

        public string IE { get; set; }

        public bool IENaoContribuinte { get; set; }

        public string SUFRAMA { get; set; }

        public string CategoriaVenda { get; set; }

		public string CategoriaVendaID { get; set; }

        public bool ClienteExterior { get; set; }

        public string IdEstrangeiro { get; set; }


        public string Logradouro { get; set; }

        public string LogradouroNumero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string CodigoMunicipio { get; set; }

        public string Pais { get; set; }

        public string CodigoPais { get; set; }

        public string CEP { get; set; }

        public string UF { get; set; }

        public string CodigoUF { get; set; }

        public string LinkLocalizacao { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string EmailFaturamento { get; set; }

        public string EmailComercial { get; set; }

        public bool Prospect { get; set; }

        public string Site { get; set; }

        public bool Cliente { get; set; }

        public bool Tecnico { get; set; }

        public bool Vendedor { get; set; }

        public bool Transportadora { get; set; }

        public bool Fonecedor { get; set; }

        public bool Representada { get; set; }

        public bool Colaborador { get; set; }

        public bool Credenciadora { get; set; }

        public bool Fabricante { get; set; }

        public string Ramo { get; set; }

        [IndexedColumn("VendedorPadraoID", "VendedorPadraoPessoaID")]
        public string VendedorPadraoID { get; set; }

        public string VendedorPadrao { get; set; }

        public string VendedorPadraoPessoaID { get; set; }

        // Adicionado por causa do modulo de ecommerce 
        public string EmailLoginEcommerce { get; set; }

        public string Senha { get; set; }

        /// <summary>
        /// Salt de chave para senhas e-commerce
        /// </summary>
        public string Salt { get; set; }

        public bool Bloqueado { get; set; }

        // Campos adicionado em 06/05/13 por solicitação de cliente varejista (aquelas moscas)
        public string NomePai { get; set; }

        public string NomeMae { get; set; }

        public string Naturalidade { get; set; }

        // *********************************************************************************
        public byte[] Foto { get; set; }

        public double ValorMinimoCompra { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataNascimento { get; set; }

        [IndexedColumn()]

        public DateTime? DataCadastro { get; set; }

        public string TabelaPrecoID { get; set; }

        public string TabelaPreco { get; set; }

        public string DadosGerais { get; set; }

        public string FaixaFuncionarios { get; set; }

        public double LimiteDeCredito { get; set; }

        [BsonIgnore]
        public double CreditoUtilizado { get; set; }

        public int PeriodicidadeNegociacao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UltimaNegociacao { get; set; }

        [IndexedColumn()]
        public string PessoaGrupoID { get; set; }

        public string PessoaGrupo { get; set; }

        public string PlanoContaReceitaID { get; set; }

        public string PlanoContaReceita { get; set; }

        // Magento API
        public string APIMagentoCustomerId { get; set; }

        public bool APIMagentoCustomerNameSynchronized { get; set; }

        //MercadoLivre
        public long MercadoLivreCustomerId { get; set; }

        [BsonIgnore]
        public string ArquivoTemporario { get; set; }

        public DtoPessoaConta[] Contas { get; set; }

        public DtoPessoaPendencia[] Pendencias { get; set; }

        public DtoPessoaReferencia[] Referencias { get; set; }

        public DtoCredenciadoraTaxas[] CredenciadoraTaxas { get; set; }

        //////////////////////////Dados da credenciadora//////////////////////////////////////////////////
        public string CredenciadoraTipoIntegracao { get; set; }

        public string CredenciadoraBancoLancamento { get; set; }

        public string CredenciadoraBancoIdLancamento { get; set; }

        //////////////////////////Dados do endereço de entrega//////////////////////////////////////////////////
        public bool EntregaExterior { get; set; }

        public bool EnderecoPadrao { get; set; }

        public bool EnderecoEntrega { get; set; }

        public bool EnderecoCobranca { get; set; }

        public string EntregaLogradouro { get; set; }

        public string EntregaLogradouroNumero { get; set; }

        public string EntregaComplemento { get; set; }

        public string EntregaBairro { get; set; }

        public string EntregaCidade { get; set; }

        public string EntregaCodigoMunicipio { get; set; }

        public string EntregaPais { get; set; }

        public string EntregaCodigoPais { get; set; }

        public string EntregaCEP { get; set; }

        public string EntregaUF { get; set; }

        public string EntregaCodigoUF { get; set; }

        //////////////////////////Dados do endereço de Cobrança//////////////////////////////////////////////////
        public bool CobrancaExterior { get; set; }

        public string CobrancaLogradouro { get; set; }

        public string CobrancaLogradouroNumero { get; set; }

        public string CobrancaComplemento { get; set; }

        public string CobrancaBairro { get; set; }

        public string CobrancaCidade { get; set; }

        public string CobrancaCodigoMunicipio { get; set; }

        public string CobrancaPais { get; set; }

        public string CobrancaCodigoPais { get; set; }

        public string CobrancaCEP { get; set; }

        public string CobrancaUF { get; set; }

        public string CobrancaCodigoUF { get; set; }

        //////////////////////////////////////////////////////////

        public string TransportadoraPadrao { get; set; }

        public string TransportadoraPadraoID { get; set; }

        //Codigo do cliente na OPAF
        public string CodigoOPAF { get; set; }

        [BsonIgnore]
        public List<DtoPessoaContato> Contatos { get; set; }

        [BsonIgnore]
        public bool EhVinculadaUsuario { get; set; }

        [BsonIgnore]
        public string UsuarioVinculado { get; set; }

        public int? WooCommerceCustomerID { get; set; }

        public string RNTRC { get; set; }

        ///Dados de pagamento
        public string FormaPagamentoNome { get; set; }

        public int? CondicaoPagamentoOpcao { get; set; }

        public string CondicaoPagamentoPersonalizado { get; set; }
    }
}