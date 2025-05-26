using App.Models;
using App.VendaERP.Core.Models;
using VendaERP.Core.Models;

namespace App.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de etiquetas que encapsula toda a lógica de negócio
    /// </summary>
    public interface IEtiquetasService
    {
        /// <summary>
        /// Cria um novo modelo de etiqueta
        /// </summary>
        /// <param name="modelo">Dados do modelo a ser criado</param>
        /// <returns>Modelo criado com ID gerado</returns>
        Task<DtoEtiquetasPadroes> CriarModeloAsync(DtoEtiquetasPadroes modelo);

        /// <summary>
        /// Atualiza um modelo de etiqueta existente
        /// </summary>
        /// <param name="id">ID do modelo</param>
        /// <param name="modelo">Dados atualizados do modelo</param>
        /// <returns>Modelo atualizado</returns>
        Task<DtoEtiquetasPadroes?> AtualizarModeloAsync(string id, DtoEtiquetasPadroes modelo);

        /// <summary>
        /// Remove um modelo de etiqueta
        /// </summary>
        /// <param name="id">ID do modelo a ser removido</param>
        /// <returns>True se removido com sucesso</returns>
        Task<bool> RemoverModeloAsync(string id);

        /// <summary>
        /// Obtém um modelo de etiqueta por ID
        /// </summary>
        /// <param name="id">ID do modelo</param>
        /// <returns>Modelo encontrado ou null</returns>
        Task<DtoEtiquetasPadroes?> ObterModeloPorIdAsync(string id);

        /// <summary>
        /// Lista modelos de etiqueta com paginação
        /// </summary>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista paginada de modelos</returns>
        Task<(List<DtoEtiquetasPadroes> modelos, int totalCount)> ListarModelosAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Processa a geração de etiquetas
        /// </summary>
        /// <param name="request">Dados da requisição de etiquetas</param>
        /// <returns>Dados processados para geração das etiquetas</returns>
        Task<EtiquetasProcessResult> ProcessarEtiquetasAsync(EtiquetasRequest request);

        /// <summary>
        /// Verifica produtos sem código de barras
        /// </summary>
        /// <param name="itens">Lista de itens a verificar</param>
        /// <returns>Lista de produtos sem código</returns>
        Task<List<ProdutoSemCodigo>> VerificarProdutosSemCodigoAsync(string[] itens);

        /// <summary>
        /// Obtém dados de um produto específico
        /// </summary>
        /// <param name="deposito">ID do depósito</param>
        /// <param name="produto">ID do produto</param>
        /// <returns>Dados do produto</returns>
        Task<ProdutoEscolhido?> ObterProdutoAsync(string deposito, string produto);
    }

    /// <summary>
    /// Classe para requisição de etiquetas
    /// </summary>
    public class EtiquetasRequest
    {
        public string Empresa { get; set; } = string.Empty;
        public string Etiqueta { get; set; } = string.Empty;
        public string ClienteFornecedor { get; set; } = string.Empty;
        public string TabelaDePreco { get; set; } = string.Empty;
        public string Deposito { get; set; } = string.Empty;
        public bool DadoLadoCodigoBarras { get; set; }
        public bool ImprimirCodigoBarras { get; set; }
        public bool ImprimirNumeroCodigoBarras { get; set; }
        public bool ImprimirCodigo { get; set; }
        public bool ImprimirNome { get; set; }
        public bool ImprimirPreco { get; set; }
        public bool PrecoComoCodigo { get; set; }
        public bool ImprimirMarca { get; set; }
        public bool ImprimirBorda { get; set; }
        public bool ImprimirLote { get; set; }
        public bool ImprimirNumeroSerie { get; set; }
        public bool GerarCodigosBarras { get; set; }
        public string[] Itens { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// Resultado do processamento de etiquetas
    /// </summary>
    public class EtiquetasProcessResult
    {
        public string NomeEmpresa { get; set; } = string.Empty;
        public DtoEtiquetasPadroes ModelEtiqueta { get; set; } = new();
        public OpcoesSelecionadas OpcoesSelecionadas { get; set; } = new();
        public List<ProdutoEscolhido> ListaItens { get; set; } = new();
    }

    /// <summary>
    /// Produto sem código de barras
    /// </summary>
    public class ProdutoSemCodigo
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
} 