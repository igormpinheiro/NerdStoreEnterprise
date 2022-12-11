using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

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
    //Sistema-indisponivel
    public IActionResult SistemaIndisponivel()
    {
        var modelError = new ErrorViewModel
        {
            Message = "O sistema está temporariamente indisponível, por favor tente novamente mais tarde ou contate nosso suporte",
            Title = "O sistema está indisponível.",
            ErrorCode = 500
        };
        return View("Error", modelError);
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelError = new ErrorViewModel();
        if (id == 500)
        {
            modelError.Message = "500: Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelError.Title = "Ocorreu um erro!";
            modelError.ErrorCode = id;
        }
        else if (id == 404)
        {
            modelError.Message = "404: A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte.";
            modelError.Title = "Ops! Página não encontrada.";
            modelError.ErrorCode = id;
        }
        else if (id == 403)
        {
            modelError.Message = "403: Você não tem permissão para fazer isto.";
            modelError.Title = "Acesso Negado";
            modelError.ErrorCode = id;
        }
        else
        {
            return StatusCode(404);
        }

        return View("Error", modelError);
    }
}