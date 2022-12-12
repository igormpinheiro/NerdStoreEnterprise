using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Cliente.API.Controllers;

public class CustomerController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public CustomerController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet("customers")]
    public async Task<ActionResult> GetAll()
    {
        var customers = await _mediatorHandler.SendCommand(new NewCustomerCommand(Guid.NewGuid(), "Fulano", "fulano@fulnao.com", "12345678910"));
        return CustomResponse(customers);

    }
}
