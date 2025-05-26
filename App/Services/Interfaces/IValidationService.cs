using App.Models;
using App.VendaERP.Core.Models;
using static App.Services.Interfaces.IEtiquetasService;

namespace App.Services.Interfaces
{
    /// <summary>
    /// Interface para serviço de validação de dados de entrada
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Valida os dados de um modelo de etiqueta
        /// </summary>
        /// <param name="modelo">Modelo a ser validado</param>
        /// <returns>Resultado da validação</returns>
        ValidationResult ValidarModelo(DtoEtiquetasPadroes modelo);

        /// <summary>
        /// Valida uma requisição de etiquetas
        /// </summary>
        /// <param name="request">Requisição a ser validada</param>
        /// <returns>Resultado da validação</returns>
        ValidationResult ValidarEtiquetasRequest(EtiquetasRequest request);

        /// <summary>
        /// Valida um ID
        /// </summary>
        /// <param name="id">ID a ser validado</param>
        /// <param name="fieldName">Nome do campo para mensagens de erro</param>
        /// <returns>Resultado da validação</returns>
        ValidationResult ValidarId(string id, string fieldName);

        /// <summary>
        /// Valida parâmetros de paginação
        /// </summary>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Resultado da validação</returns>
        ValidationResult ValidarPaginacao(int pageNumber, int pageSize);

        /// <summary>
        /// Valida uma lista de itens
        /// </summary>
        /// <param name="itens">Lista de itens a validar</param>
        /// <returns>Resultado da validação</returns>
        ValidationResult ValidarItens(string[] itens);

        /// <summary>
        /// Sanitiza uma string removendo caracteres perigosos
        /// </summary>
        /// <param name="input">String a ser sanitizada</param>
        /// <returns>String sanitizada</returns>
        string SanitizeString(string input);

        /// <summary>
        /// Valida e converte um valor decimal
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <param name="fieldName">Nome do campo</param>
        /// <returns>Valor convertido ou null se inválido</returns>
        decimal? ValidarEConverterDecimal(string? value, string fieldName);
    }

    /// <summary>
    /// Resultado de uma validação
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();

        public static ValidationResult Success() => new() { IsValid = true };
        
        public static ValidationResult Failure(string error) => new() 
        { 
            IsValid = false, 
            Errors = new List<string> { error } 
        };
        
        public static ValidationResult Failure(List<string> errors) => new() 
        { 
            IsValid = false, 
            Errors = errors 
        };

        public void AddError(string error)
        {
            IsValid = false;
            Errors.Add(error);
        }
    }
} 