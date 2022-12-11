namespace NSE.Cliente.API.Application.Commands;

public class NewCustomerCommand
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }
}
