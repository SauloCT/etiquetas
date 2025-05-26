using App.Models;
using App.Services.Interfaces;
using App.VendaERP.Core.Models;
using static App.Services.Interfaces.IValidationService;
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ValidationResult ValidarModelo(DtoEtiquetasPadroes modelo)
        {
            try
            {
                var errors = new List<string>();

                if (modelo == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Modelo não pode ser nulo",
                        Errors = new List<string> { "Modelo não pode ser nulo" }
                    };
                }

                // Validar nome
                if (string.IsNullOrWhiteSpace(modelo.Nome))
                {
                    errors.Add("Nome do modelo é obrigatório");
                }
                else if (modelo.Nome.Length > 100)
                {
                    errors.Add("Nome do modelo não pode ter mais de 100 caracteres");
                }

                // Validar papel
                if (!Enum.IsDefined(typeof(TipoPapel), (int)modelo.Papel))
                {
                    errors.Add("Tipo de papel inválido");
                }

                // Validar dimensões do papel
                if (modelo.LarguraPapel <= 0)
                {
                    errors.Add("Largura do papel deve ser maior que zero");
                }

                if (modelo.AlturaPapel <= 0)
                {
                    errors.Add("Altura do papel deve ser maior que zero");
                }

                // Validar dimensões opcionais se fornecidas
                if (modelo.Largura.HasValue && modelo.Largura <= 0)
                {
                    errors.Add("Largura da etiqueta deve ser maior que zero");
                }

                if (modelo.Altura.HasValue && modelo.Altura <= 0)
                {
                    errors.Add("Altura da etiqueta deve ser maior que zero");
                }

                // Validar margens se fornecidas
                if (modelo.MargemEsquerda.HasValue && modelo.MargemEsquerda < 0)
                {
                    errors.Add("Margem esquerda não pode ser negativa");
                }

                if (modelo.MargemSuperior.HasValue && modelo.MargemSuperior < 0)
                {
                    errors.Add("Margem superior não pode ser negativa");
                }

                // Validar espaçamentos se fornecidos
                if (modelo.EspacamentoHorizontal.HasValue && modelo.EspacamentoHorizontal < 0)
                {
                    errors.Add("Espaçamento horizontal não pode ser negativo");
                }

                if (modelo.EspacamentoVertical.HasValue && modelo.EspacamentoVertical < 0)
                {
                    errors.Add("Espaçamento vertical não pode ser negativo");
                }

                // Validar zoom se fornecido
                if (modelo.ZoomImpressao.HasValue && (modelo.ZoomImpressao <= 0 || modelo.ZoomImpressao > 500))
                {
                    errors.Add("Zoom de impressão deve estar entre 1 e 500");
                }

                // Validar tamanhos de fonte se fornecidos
                if (modelo.TamanhoFonte.HasValue && (modelo.TamanhoFonte <= 0 || modelo.TamanhoFonte > 72))
                {
                    errors.Add("Tamanho da fonte deve estar entre 1 e 72");
                }

                if (modelo.TamanhoPreco.HasValue && (modelo.TamanhoPreco <= 0 || modelo.TamanhoPreco > 72))
                {
                    errors.Add("Tamanho da fonte do preço deve estar entre 1 e 72");
                }

                // Validar altura do código de barras se fornecida
                if (modelo.AlturaEAN.HasValue && modelo.AlturaEAN <= 0)
                {
                    errors.Add("Altura do código de barras deve ser maior que zero");
                }

                return new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    ErrorMessage = string.Join("; ", errors),
                    Errors = errors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar modelo");
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Erro interno na validação",
                    Errors = new List<string> { "Erro interno na validação" }
                };
            }
        }

        public ValidationResult ValidarEtiquetasRequest(EtiquetasRequest request)
        {
            try
            {
                var errors = new List<string>();

                if (request == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Request não pode ser nulo",
                        Errors = new List<string> { "Request não pode ser nulo" }
                    };
                }

                // Validar campos obrigatórios
                if (string.IsNullOrWhiteSpace(request.Empresa))
                {
                    errors.Add("Empresa é obrigatória");
                }

                if (string.IsNullOrWhiteSpace(request.Etiqueta))
                {
                    errors.Add("Modelo de etiqueta é obrigatório");
                }

                // Validar itens
                if (request.Itens == null || request.Itens.Length == 0)
                {
                    errors.Add("Pelo menos um item deve ser selecionado");
                }

                return new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    ErrorMessage = string.Join("; ", errors),
                    Errors = errors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar request de etiquetas");
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Erro interno na validação do request",
                    Errors = new List<string> { "Erro interno na validação do request" }
                };
            }
        }

        public ValidationResult ValidarId(string id, string fieldName)
        {
            try
            {
                var errors = new List<string>();

                if (string.IsNullOrWhiteSpace(id))
                {
                    errors.Add($"{fieldName} é obrigatório");
                }
                else if (!ObjectId.TryParse(id, out _))
                {
                    errors.Add($"{fieldName} deve ter um formato válido");
                }

                return new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    ErrorMessage = string.Join("; ", errors),
                    Errors = errors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar ID: {Id}", id);
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Erro interno na validação do ID",
                    Errors = new List<string> { "Erro interno na validação do ID" }
                };
            }
        }

        public ValidationResult ValidarPaginacao(int pageNumber, int pageSize)
        {
            try
            {
                var errors = new List<string>();

                if (pageNumber <= 0)
                {
                    errors.Add("Número da página deve ser maior que zero");
                }

                if (pageSize <= 0)
                {
                    errors.Add("Tamanho da página deve ser maior que zero");
                }
                else if (pageSize > 100)
                {
                    errors.Add("Tamanho da página não pode ser maior que 100");
                }

                return new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    ErrorMessage = string.Join("; ", errors),
                    Errors = errors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar paginação");
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Erro interno na validação da paginação",
                    Errors = new List<string> { "Erro interno na validação da paginação" }
                };
            }
        }

        public ValidationResult ValidarItens(string[] itens)
        {
            try
            {
                var errors = new List<string>();

                if (itens == null)
                {
                    errors.Add("Lista de itens não pode ser nula");
                }
                else if (itens.Length == 0)
                {
                    errors.Add("Pelo menos um item deve ser fornecido");
                }
                else if (itens.Length > 1000)
                {
                    errors.Add("Número máximo de itens é 1000");
                }

                return new ValidationResult
                {
                    IsValid = errors.Count == 0,
                    ErrorMessage = string.Join("; ", errors),
                    Errors = errors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar itens");
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Erro interno na validação dos itens",
                    Errors = new List<string> { "Erro interno na validação dos itens" }
                };
            }
        }

        public string SanitizeString(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return string.Empty;
                }

                // Remover caracteres perigosos e normalizar
                var sanitized = input.Trim();
                
                // Remover caracteres de controle
                sanitized = Regex.Replace(sanitized, @"[\x00-\x1F\x7F]", "");
                
                // Remover múltiplos espaços
                sanitized = Regex.Replace(sanitized, @"\s+", " ");
                
                // Escapar caracteres HTML básicos
                sanitized = sanitized
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("'", "&#x27;")
                    .Replace("&", "&amp;");

                return sanitized;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao sanitizar string: {Input}", input);
                return string.Empty;
            }
        }

        public decimal? ValidarEConverterDecimal(string? value, string fieldName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }

                // Normalizar separadores decimais
                var normalizedValue = value.Replace(',', '.');

                if (decimal.TryParse(normalizedValue, System.Globalization.NumberStyles.Float, 
                    System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                {
                    return result;
                }

                _logger.LogWarning("Falha ao converter valor decimal: {Value} para campo {FieldName}", value, fieldName);
                throw new ArgumentException($"Valor inválido para {fieldName}: {value}");
            }
            catch (ArgumentException)
            {
                throw; // Re-throw validation errors
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar e converter decimal: {Value}", value);
                throw new ArgumentException($"Erro ao processar {fieldName}");
            }
        }
    }
} 