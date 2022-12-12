using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace NSE.WebAPI.Core.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    private ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddErrorToStack(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected bool ValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddErrorToStack(string erro)
    {
        Errors.Add(erro);
    }

    protected void CleanErrors()
    {
        Errors.Clear();
    }
}