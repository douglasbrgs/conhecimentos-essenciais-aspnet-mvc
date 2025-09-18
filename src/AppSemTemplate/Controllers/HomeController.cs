using AppSemTemplate.Configuration;
using AppSemTemplate.Models;
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

        [Route("error/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelError.Title = "Ocorreu um erro.";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "A página que está procurando não existe! <br/> Em caso de dúvida entre em contato com nosso suporte.";
                modelError.Title = "Ops! Página não encontrada.";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "Você não tem permissão para fazer isso.";
                modelError.Title = "Acesso Negado";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelError);
        }

        [Route("teste")]
        public IActionResult Teste()
        {
            throw new Exception("ALGO ERRADO NÃO ESTAVA CERTO!");

            return View("Index");
        }
    }
}
