using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppSemTemplate.Extensions
{
    /// <summary>
    /// Filtro de auditoria que regista acessos de usuários autenticados.
    /// </summary>
    public class FiltroAuditoria : IActionFilter
    {
        private readonly ILogger<FiltroAuditoria> _logger;

        public FiltroAuditoria(ILogger<FiltroAuditoria> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Executado após a execução da action. Regista, via ILogger.LogWarning,
        /// o nome do usuário e a URL acessada quando o usuário está autenticado.
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acessou: " +
                              context.HttpContext.Request.GetDisplayUrl();

                _logger.LogWarning(message);
            }
        }

        /// <summary>
        /// Executado antes da execução da action.
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // O que fazer durante a execução da Action?
        }
    }
}
