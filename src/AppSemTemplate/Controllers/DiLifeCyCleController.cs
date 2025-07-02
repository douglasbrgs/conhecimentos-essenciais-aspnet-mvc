using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Controllers
{
    [Route("teste-di")]
    public class DiLifeCyCleController : Controller
    {
        public IOperacao Operacao { get; private set; }

        public DiLifeCyCleController(IOperacao operacao)
        {
            Operacao = operacao;
        }

        public IActionResult Index()
        {
            var teste = Operacao;

            return View();
        }
    }
}
