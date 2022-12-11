using FluentValidation.Results;

namespace NSE.Core.Messages;

public abstract class Command : Message
{
    public DateTime TimeStamp { get; private set; }

    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        TimeStamp = DateTime.Now;
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}

public abstract class Message
{
    public string MessageType { get; protected set; }
    public Guid AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}