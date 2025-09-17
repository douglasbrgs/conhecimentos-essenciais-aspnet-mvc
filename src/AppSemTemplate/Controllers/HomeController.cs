using AppSemTemplate.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace AppSemTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;

        private readonly ApiConfiguration ApiConfig;

        public HomeController(IConfiguration configuration,
            IOptions<ApiConfiguration> options)
        {
            Configuration = configuration;
            ApiConfig = options.Value;
        }

        public ActionResult Index()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var apiConfig = new ApiConfiguration();

            Configuration
                .GetSection(ApiConfiguration.ConfigName)
                .Bind(apiConfig);

            // Fazendo bind com modelo
            var secret = apiConfig.UserSecret;

            // Diretamente da chave
            var user = Configuration["ApiConfiguration:UserKey"];

            // Lendo arquivo de configuracao via options
            var domain = ApiConfig.Domain;

            return View();
        }
    }
}
