using FluentValidation.Results;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Cliente.API.Services;

public class UserRegisteredIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public UserRegisteredIntegrationHandler(IServiceProvider serviceProvider,
        IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(
            async request => await NewCustomer(request));

        _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }

    private async Task<ResponseMessage> NewCustomer(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new NewCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

        ValidationResult success;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            success = await mediator.SendCommand(customerCommand);
        }

        return new ResponseMessage(success);
    }
}