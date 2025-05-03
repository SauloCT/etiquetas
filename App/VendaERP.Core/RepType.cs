
namespace VendaERP.Core
{
    public enum RepType
    {
        // Produtos
        Produto,
        ProdutoComposto,
        ProdutoImagem,
        ProdutoCategoria,
        ProdutoTabelaPreco,
        ProdutoTamanho,
        ProdutoMarca,
        ProdutoPreco,
        ProdutoAttributo,
        ProdutoGrade,
        ProdutoSimilar,
        ProdutoCatalogo,


        // Estoque
        DepositoEstoque,
        DepositoProduto,
        DepositoProdutoLote,
        EstoqueSaida,
        EstoqueEntrada,
        EstoqueUnidade,
        EstoqueMovEntreDepositos,
        EstoqueMovEntreDepositosProduto,

        // Compra
        OrdemCompraItem,
        CompraRapida,
        OrdemCompra,

        // Entrada
        NotaEntradaProduto,
        NotaEntrada,
        NotaEntradaDuplicata,

        // Venda
        VendaProduto,
        Venda,
        VendaEquipamento,
        VendaVeiculo,
        VendaLaudo,
        VendaRapida,
        VendaStatus,
        VendaTerceiros,
        VendaCategoria,

        //Ordem Expedição
        OrdemExpedicao,
        OrdemExpedicaoItem,

        //PDV
        OperacaoPDV,
        VendaConsignada,


        //Marketing
        EmailMarketingQuarentena,

        //ESTATISTICAS
        CadastroPassos,


        // Configurações
        Configuracoes,

        // Financeiro
        Banco,
        Lancamento,
        PlanoDeContas,
        GrupoDRE,
        FormaPagamento,
        CentroCustos,
        Boletos,
        BoletosRemessa,
        LancamentoGrupo,
        TransferenciaBancaria,

        //Pessoas
        Pessoas,
        PessoaContato,
        PessoaGrupo,


        //MRP
        Etapa,
        ProcessoProdutivo,
        ProdutoQualidade,
        OrdemProducao,
        OrdemProducaoHistorico,
        OrdemProducaoComposicao,


        //Fiscal
        GrupoTributario,
        NCM,
        CFOP,
        NotaFiscalEletronica,
        SpeedFiscal,
        InutilizacaoNumeracaoNFe,
        LoteNotaFiscalEletronica,
        EventoNotaFiscalEletronica,
        OperacaoFiscal,
        OperacaoFiscalNCM,
        ConfiguracaoNFe,
        LogNFe,
        InutilizacaoNumeracaoCTe,
        LoteCTe,
        CTe,
        EventoCTe,
        NFSe,


        // Serviços
        ServicoTipo,
        OrdemServico,

        // Arquivos
        Arquivo,
        FileDocsXUser,

        //Empresa
        Empresa,
        EmpresaModelo,

        // CRM
        Oportunidade,
        OportunidadeEvento,
        Campanha,
        Case,

        //Agenda
        Agenda,

        //Ecommerce
        EcommerceVisualizacaoProduto,
        EcommerceBuscaLoja,
        EcommerceTransacaoPagamento,
        EcommerceBoletoPagamento,
        EcommerceCartaoPagamento,
        EcommerceCupomDesconto,
        EcommerceCompreJunto,
        EcommerceBanner,
        EcommerceFrete,

        //Contrato
        Contratos,
        ContratoTipo,

        //RH
        Colaborador,
        ColaboradorTipo,
        ColaboradorCategoria,
        ColaboradorProfissao,
        ColaboradorExpediente,
        ColaboradorExperiencia,
        ColaboradorCurso,
        ColaboradorCompetencia,
        ColaboradorFolha,
        ColaboradorFolhaItem,
        ColaboradorFolhaFalta,
        ColaboradorFolhaHoraExtra,
        Departamento,


        // USADOS NA BASE DO SISTEMA

        //Fiscal
        CodigoFiscal,
        MediaImpostosIBPT,

        //Suporte
        TicketSuporte,
        TicketMensagem,
        MensagemSistema,
        MensagemSistemaUsuario,
        BlogPost,
        FAQ,
        PrimeirosPassos,
        FAQ2,
        FAQCategoria,
        SIGEOportunidade,
        SIGEOportunidadeHistorico,
        Depoimento,
        DocumentacaoAPI,
        PerguntasFrequentesAdmin,


        // Gerencia
        Gerenciador,
        FerramentasDoSistema,
        ModuloDoSistema,
        Licenca,
        Adicional,
        LicencaAdicional,
        PermissoesDoUsuario,
        PermissoesDaLicenca,
        LicencaDoSistema,
        TempUser,
        ViewLicencas,
        ViewDesistencias,

        SIGEDebugLog,

        //Endereços
        CEP,
        Estado,
        Ceps,
        Paises,


        //Financeiro
        TransacaoPagamento,
        BoletoPagamento,

        //API
        APIApp,
        APIAppKey,

        //WebServiceNFe
        WebServiceNFe,

        //Arquivos Temporários, exemplo foto de uma pessoa que não foi salva.
        ArquivoTemporario,

        //CupomDesconto ERP
        ERPCupomDesconto,

        //Projeto
        Projeto,

        //Cotacao
        Cotacao,
        CotacaoItem,
        CotacaoCotista,
        CotacaoItemCotado,

        //UsuarioPerfil
        UsuarioPerfil,

        //RelatorioPersonalizado
        RelatorioPersonalizado,

    }


}