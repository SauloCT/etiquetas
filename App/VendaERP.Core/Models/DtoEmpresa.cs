using MongoDB.Bson.Serialization.Attributes;
using System;

namespace VendaERP.Core.Models
{
    public class DtoEmpresa : EntityLastUpdate
    {
        //public string EmpresaID { get; set; }
        public string Gerenciador { get; set; }


        public string NomeFantasia { get; set; }


        public string RazaoSocial { get; set; }


        public string CNPJ { get; set; }

        public string InscricaoEstadual { get; set; }

        public string InscricaoEstadualST { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }


        public string Cidade { get; set; }


        public string UF { get; set; }

        public string Ramo { get; set; }


        public string Email { get; set; }

        public string EmailEmissorBoletos { get; set; }

        public string Site { get; set; }

        public string CEP { get; set; }

        public string Telefone { get; set; }

        public string RNTRC { get; set; }

        public double Aliquota { get; set; }

        public double IRPJ { get; set; }

        public double CSLL { get; set; }

        public double Cofins { get; set; }

        public double PIS_Pasep { get; set; }

        public double CPP { get; set; }

        public double ICMS { get; set; }

        public double IPI { get; set; }

        public double ISS { get; set; }


        public bool Padrao { get; set; }

        public double JurosPadrao { get; set; }

        public int CodigoMunicipio { get; set; }

        public int CodigoUF { get; set; }

        public string Bairro { get; set; }

        public string MatrizID { get; set; }

        public string Matriz { get; set; }


        public string Hierarquia { get; set; }

        public byte[] Logo { get; set; }

        public string RegimeTributario { get; set; }

        public bool GeraSPED_PIS_COFINS { get; set; }

        public string AtividadePreponderante { get; set; }

        public string RegimeApuracao { get; set; }

        public string IncidenciaTributaria { get; set; }

        public string PISCOFINS_Entrada { get; set; }

        public string PISCOFINS_Saida { get; set; }

        public string PoliticasVendas { get; set; }

        public string DepositoPadraoID { get; set; }

        public string DepositoPadrao { get; set; }

        /***************************PDV***************************************/

        public string TabelaPrecoPDVID { get; set; }

        public string TabelaPrecoPDV { get; set; }
        public string BancoPDVID { get; set; }

        public string BancoPDV { get; set; }

        public string BancoPadraoID { get; set; }

        public string BancoPadrao { get; set; }

        public string CredenciadoraCartaoPDVID { get; set; }

        public string CredenciadoraCartaoPDV { get; set; }

        public string PlanoDeContaPDVID { get; set; }

        public string PlanoDeContaPDV { get; set; }

        public bool PermitirEstoqueNegativoPDV { get; set; }

        public string DocumentoEmissaoPDV { get; set; }

        public string SenhaDescontoPDV { get; set; }

        public string PadraoBalancaPDV { get; set; }

        public bool HabilitarBalancaPDV { get; set; }

        /**********************************************************************/

        public string InformativoPadraoOS { get; set; }

        public string Contador_Nome { get; set; }

        public string Contador_CPF { get; set; }

        public string Contador_CRC { get; set; }

        public string Contador_CNPJ { get; set; }

        public string Contador_CEP { get; set; }

        public string Contador_Logradouro { get; set; }

        public string Contador_LogradouroNumero { get; set; }

        public string Contador_Complemento { get; set; }

        public string Contador_Bairro { get; set; }

        public string Contador_Telefone { get; set; }

        public string Contador_Fax { get; set; }

        public string Contador_Email { get; set; }

        public string Contador_Municipio { get; set; }

        public string Contador_CodigoMunicipio { get; set; }

        public string Contador_UF { get; set; }

        public string Contador_CodigoUF { get; set; }

        public double MultaPadrao { get; set; }

        public string NFE_InformacaoComplementarPadrao { get; set; }

        public string NFCE_InformacaoComplementarPadrao { get; set; }

        public string NFSE_InformacaoComplementarPadrao { get; set; }

        public string InscricaoMunicipal { get; set; }

        public bool EhEmpresaDeCobranca { get; set; }

        /**************************** DADOS INVOICY (NFSe)**************************************************************/
        public bool NaoPermitirEmissaoNFSe { get; set; }

        public string CodCKInvoicy { get; set; }

        public string CodEmpresaInvoicy { get; set; }

        public bool NFSeEnviarEmailAoEfetivar { get; set; }

        public bool NFSeNaoInformarEmailTomadorNoXML { get; set; }

        public bool NFSeIgnorarBankersRounding { get; set; }

        public bool NFSeConjugada { get; set; }

        public bool NFSeCodigoServicoIgualCodTributacaoMunicipio { get; set; }

        public string CNAE { get; set; }

        public string UsuarioWebServicePrefeitura { get; set; }

        public string SenhaWebServicePrefeitura { get; set; }

        /***************************************************************************************************************/
        [BsonIgnore]
        public string ArquivoTemporarioID { get; set; }

        /**************************** E-mails Padrões**************************************************************/

        public string EmailPadraoCompras { get; set; }

        public string EmailPadraoPedidos { get; set; }

        public string EmailPadraoNFe { get; set; }

        /****************************Formatação E-mails Padrões**************************************************************/

        public string EmailAssuntoNFE { get; set; }

        public string EmailMensagemNFE { get; set; }

        public string EmailAssuntoBoleto { get; set; }

        public string EmailMensagemBoleto { get; set; }

        public string EmailAssuntoVenda { get; set; }

        public string EmailMensagemVenda { get; set; }

        public string EmailAssuntoOrdemCompra { get; set; }

        public string EmailMensagemOrdemCompra { get; set; }

        public string EmailAssuntoCotacao { get; set; }

        public string EmailMensagemCotacao { get; set; }

        public string EmailAssuntoCobranca { get; set; }

        public string EmailMensagemCobranca { get; set; }

        public string EmailAssuntoOS { get; set; }

        public string EmailMensagemOS { get; set; }

        public string EmailAssuntoReciboOS { get; set; }

        public string EmailMensagemReciboOS { get; set; }

        /****************************Dados SIGEP WEB***************************************************/

        public string SIGEPNumeroContrato { get; set; }

        public string SIGEPNumeroCartao { get; set; }

        public string SIGEPUsuario { get; set; }

        public string SIGEPSenha { get; set; }

        public string SIGEPCNPJ { get; set; }

        public string SIGEPCodAdministrativo { get; set; }

        public string SIGEPCodDiretoria { get; set; }

        public DateTime SIGEPUltimaAtualizacao { get; set; }

        public int SIGEPPrazoMinimoEntrega { get; set; }

        /****************************Dados Para Envio de Email***************************************************/
        public DtoConfiguracaoEnvioEmail ConfiguracaoEmail { get; set; }

        /*****************************Dados de Autorização do XML ******************************************/
        public bool AutorizarDadosXML { get; set; }

        public string VersaoDadosAplicativoComercial { get; set; }

        public string TipoDashboard { get; set; }

        public string TipoEcommerce { get; set; }

    }

    public class DtoConfiguracaoEnvioEmail : Entity
    {
        public string Email { get; set; }

        public string WhiteLabelId { get; set; }

        public string ProvedorEmail { get; set; }

        public bool UtilizarProtocoloSSL { get; set; }

        /// <summary>
        /// SMTP, POP, IMAP
        /// </summary>
        public string ProtocoloComunicacaoEmail { get; set; }

        public string EnderecoServidor { get; set; }

        public int PortaServidor { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }
    }
}