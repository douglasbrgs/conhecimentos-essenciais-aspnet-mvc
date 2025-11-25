using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;

namespace AppSemTemplate.Configuration
{
    public static class LoggingConfig
    {
        public static WebApplicationBuilder AddElmahConfiguration(this WebApplicationBuilder builder)
        {
            // Rastreia exceções
            builder.Services.Configure<ElmahIoOptions>(builder.Configuration.GetSection("ElmahIo"));
            builder.Services.AddElmahIo();

            // Elmah como serviço de logging padrão
            builder.Logging.Services.Configure<ElmahIoProviderOptions>(builder.Configuration.GetSection("ElmahIo"));
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddElmahIo();

            // Limitar o nivel de log que sera enviado ao Elmah
            builder.Logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);

            return builder;
        }
    }
}
