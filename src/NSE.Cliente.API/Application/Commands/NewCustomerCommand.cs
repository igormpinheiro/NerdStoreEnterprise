using FluentValidation;
using NSE.Core.DomainObjects;
using NSE.Core.Messages;

namespace NSE.Cliente.API.Application.Commands;

public class NewCustomerCommand : Command
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public NewCustomerCommand(Guid id, string name, string email, string cpf)
    {
        Id = AggregateId = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new NewCustomerValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class NewCustomerValidation : AbstractValidator<NewCustomerCommand>
{
    public NewCustomerValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("O ID do cliente não pode ser vazio");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome do cliente não foi informado");

        RuleFor(c => c.Email)
            .Must(BeAValidEmail)
            .WithMessage("O e-mail não é válido");

        RuleFor(c => c.Cpf)
            .Must(BeAValidCpf)
            .WithMessage("O CPF não é valido");
    }

    protected static bool BeAValidEmail(string email)
    {
        return Email.Validate(email);
    }

    protected static bool BeAValidCpf(string cpf)
    {
        return CPF.Validate(cpf);
    }
}