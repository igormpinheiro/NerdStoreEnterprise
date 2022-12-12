using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Events;
using NSE.Cliente.API.Models;
using NSE.Core.Messages;

namespace NSE.Cliente.API.Application.Commands;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<NewCustomerCommand, ValidationResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ValidationResult> Handle(NewCustomerCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var customer = new Customer(message.Id, message.Name, message.Email, message.Cpf);

        var customerExist = await _customerRepository.GetByCPF(customer.Cpf.Number);

        if (customerExist != null)
        {
            AddError("Já existe um cliente com o CPF informado");
            return ValidationResult;
        }

        _customerRepository.Add(customer);

        customer.AddEvent(new NewCustomerAddedEvent(message.Id, message.Name, message.Email, message.Cpf));

        return await PersistData(_customerRepository.UnitOfWork);
    }

    //public async Task<ValidationResult> Handle(AddAddressCommand message, CancellationToken cancellationToken)
    //{
    //    if (!message.IsValid()) return message.ValidationResult;

    //    var endereco = new Address(message.StreetAddress, message.BuildingNumber, message.SecondaryAddress, message.Neighborhood, message.ZipCode, message.City, message.State, message.CustomerId);
    //    _customerRepository.AddAddress(endereco);

    //    return await PersistData(_customerRepository.UnitOfWork);
    //}
}