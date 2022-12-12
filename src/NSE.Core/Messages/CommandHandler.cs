using FluentValidation.Results;
using NSE.Core.Data;

namespace NSE.Core.Messages;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult { get; private set; }

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected void AddError(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ValidationResult.Errors.Add(error);
        }
    }

    protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
    {
        if (!await uow.Commit()) AddError("An error occurred while trying to persist data");

        return ValidationResult;
    }

    protected bool IsValid()
    {
        return ValidationResult.IsValid;
    }
}