using GuedesTime.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();
			modelErro.ErroCode = id;

			switch (id)
			{
				
				case 500:
					modelErro.Titulo = "Ocorreu um erro!";
					modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
					break;
				case 404:
					modelErro.Titulo = "Ops! Página não encontrada.";
					modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
					break;
				case 403:
					modelErro.Titulo = "Acesso Negado";
					modelErro.Mensagem = "Você não tem permissão para fazer isto.";
					break;
				default:
					return StatusCode(500);
			}

			return View("Error", modelErro);
        }
    }
}
