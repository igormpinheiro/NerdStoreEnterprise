using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.Identidade.API.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    private ICollection<string> Errors = new List<string>();
    
    public ActionResult CustomResponse(object result = null)
    {
        if(OperacaoValida())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Mensagens", Errors.ToArray() }
        }));
    }
    
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AdicionarErroProcessamento(error.ErrorMessage);
        }
        
        return CustomResponse();
    }
    
    protected bool OperacaoValida()
    {
        return !Errors.Any();
    }
    
    protected void AdicionarErroProcessamento(string erro)
    {
        Errors.Add(erro);
    }
    
    protected void LimparErrosProcessamento()
    {
        Errors.Clear();
    }
}