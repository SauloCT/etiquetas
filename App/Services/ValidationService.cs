using App.Services.Interfaces;
using App.VendaERP.Core.Models;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace App.Services
{
    /// <summary>
    /// Serviço de validação de dados de entrada
    /// </summary>
    public class ValidationService : IValidationService
    {
        private readonly ILogger<ValidationService> _logger;

        public ValidationService(ILogger<ValidationService> logger)
        {
            _logger = logger;
        }

        public ValidationResult ValidarModelo(DtoEtiquetasPadroes modelo)
        {
            var result = new ValidationResult { IsValid = true };

            if (modelo == null)
            {
                result.AddError("Modelo não pode ser nulo");
                return result;
            }

            // Validar nome
            if (string.IsNullOrWhiteSpace(modelo.Nome))
            {
                result.AddError("Nome é obrigatório");
            }
            else if (modelo.Nome.Length > 100)
            {
                result.AddError("Nome não pode ter mais de 100 caracteres");
            }

            // Validar papel
            if (!Enum.IsDefined(typeof(TipoPapel), (int)modelo.Papel))
            {
                result.AddError("Tipo de papel inválido");
            }

            // Validar dimensões do papel
            if (modelo.LarguraPapel <= 0)
            {
                result.AddError("Largura do papel deve ser maior que zero");
            }

            if (modelo.AlturaPapel <= 0)
            {
                result.AddError("Altura do papel deve ser maior que zero");
            }

            // Validar dimensões da etiqueta (se fornecidas)
            if (modelo.Largura.HasValue && modelo.Largura <= 0)
            {
                result.AddError("Largura da etiqueta deve ser maior que zero");
            }

            if (modelo.Altura.HasValue && modelo.Altura <= 0)
            {
                result.AddError("Altura da etiqueta deve ser maior que zero");
            }

            // Validar espaçamentos (se fornecidos)
            if (modelo.EspacamentoHorizontal.HasValue && modelo.EspacamentoHorizontal < 0)
            {
                result.AddError("Espaçamento horizontal não pode ser negativo");
            }

            if (modelo.EspacamentoVertical.HasValue && modelo.EspacamentoVertical < 0)
            {
                result.AddError("Espaçamento vertical não pode ser negativo");
            }

            // Validar margens (se fornecidas)
            if (modelo.MargemEsquerda.HasValue && modelo.MargemEsquerda < 0)
            {
                result.AddError("Margem esquerda não pode ser negativa");
            }

            if (modelo.MargemSuperior.HasValue && modelo.MargemSuperior < 0)
            {
                result.AddError("Margem superior não pode ser negativa");
            }

            // Validar zoom (se fornecido)
            if (modelo.ZoomImpressao.HasValue && (modelo.ZoomImpressao <= 0 || modelo.ZoomImpressao > 500))
            {
                result.AddError("Zoom deve estar entre 1 e 500");
            }

            // Validar tamanhos de fonte (se fornecidos)
            if (modelo.TamanhoFonte.HasValue && (modelo.TamanhoFonte <= 0 || modelo.TamanhoFonte > 72))
            {
                result.AddError("Tamanho da fonte deve estar entre 1 e 72");
            }

            if (modelo.TamanhoPreco.HasValue && (modelo.TamanhoPreco <= 0 || modelo.TamanhoPreco > 72))
            {
                result.AddError("Tamanho da fonte do preço deve estar entre 1 e 72");
            }

            // Validar altura das barras (se fornecida)
            if (modelo.AlturaEAN.HasValue && modelo.AlturaEAN <= 0)
            {
                result.AddError("Altura das barras deve ser maior que zero");
            }

            return result;
        }

        public ValidationResult ValidarEtiquetasRequest(EtiquetasRequest request)
        {
            var result = new ValidationResult { IsValid = true };

            if (request == null)
            {
                result.AddError("Requisição não pode ser nula");
                return result;
            }

            // Validar empresa
            var empresaValidation = ValidarId(request.Empresa, "Empresa");
            if (!empresaValidation.IsValid)
            {
                result.Errors.AddRange(empresaValidation.Errors);
            }

            // Validar etiqueta
            var etiquetaValidation = ValidarId(request.Etiqueta, "Modelo de etiqueta");
            if (!etiquetaValidation.IsValid)
            {
                result.Errors.AddRange(etiquetaValidation.Errors);
            }

            // Validar itens
            var itensValidation = ValidarItens(request.Itens);
            if (!itensValidation.IsValid)
            {
                result.Errors.AddRange(itensValidation.Errors);
            }

            return result;
        }

        public ValidationResult ValidarId(string id, string fieldName = "ID")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ValidationResult.Failure($"{fieldName} é obrigatório");
            }

            if (!ObjectId.TryParse(id, out _))
            {
                return ValidationResult.Failure($"{fieldName} tem formato inválido");
            }

            return ValidationResult.Success();
        }

        public ValidationResult ValidarPaginacao(int pageNumber, int pageSize)
        {
            var result = new ValidationResult { IsValid = true };

            if (pageNumber < 1)
            {
                result.AddError("Número da página deve ser maior que zero");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                result.AddError("Tamanho da página deve estar entre 1 e 100");
            }

            return result;
        }

        public ValidationResult ValidarItens(string[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                return ValidationResult.Failure("Ao menos um item deve ser inserido");
            }

            var result = new ValidationResult { IsValid = true };

            for (int i = 0; i < itens.Length; i++)
            {
                var item = itens[i];
                if (string.IsNullOrWhiteSpace(item))
                {
                    result.AddError($"Item {i + 1} não pode estar vazio");
                    continue;
                }

                var parts = item.Split(',');
                if (parts.Length < 2)
                {
                    result.AddError($"Item {i + 1} tem formato inválido");
                    continue;
                }

                // Validar ID do produto
                if (!ObjectId.TryParse(parts[0], out _))
                {
                    result.AddError($"Item {i + 1}: ID do produto inválido");
                }

                // Validar quantidade
                if (!int.TryParse(parts[1], out int quantidade) || quantidade <= 0)
                {
                    result.AddError($"Item {i + 1}: Quantidade deve ser um número inteiro positivo");
                }
            }

            return result;
        }

        public string SanitizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remove caracteres perigosos
            var sanitized = input
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#x27;")
                .Replace("/", "&#x2F;");

            // Remove caracteres de controle
            sanitized = Regex.Replace(sanitized, @"[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]", "");

            return sanitized.Trim();
        }

        public double? ValidarEConverterDecimal(string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            try
            {
                // Sanitizar a string primeiro
                var sanitizedValue = SanitizeString(value);
                
                // Substituir ponto por vírgula para conversão
                sanitizedValue = sanitizedValue.Replace(".", ",");

                if (double.TryParse(sanitizedValue, out double result))
                {
                    if (result < 0)
                    {
                        _logger.LogWarning("Valor negativo fornecido para {FieldName}: {Value}", fieldName, value);
                        return null;
                    }
                    return result;
                }

                _logger.LogWarning("Falha ao converter valor para {FieldName}: {Value}", fieldName, value);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar e converter decimal para {FieldName}: {Value}", fieldName, value);
                return null;
            }
        }
    }
} 