using System.Text.Json;

namespace App.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado na requisição {RequestPath} {Method}", 
                    context.Request.Path, context.Request.Method);
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var response = exception switch
            {
                ArgumentException => new { StatusCode = 400, Message = "Dados inválidos fornecidos" },
                UnauthorizedAccessException => new { StatusCode = 401, Message = "Acesso não autorizado" },
                KeyNotFoundException => new { StatusCode = 404, Message = "Recurso não encontrado" },
                InvalidOperationException => new { StatusCode = 409, Message = "Operação inválida" },
                _ => new { StatusCode = 500, Message = "Erro interno do servidor" }
            };

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
} 