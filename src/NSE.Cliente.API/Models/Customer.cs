using NSE.Core.DomainObjects;

namespace NSE.Cliente.API.Models;

public class Customer : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public CPF Cpf { get; private set; }
    public bool Deleted { get; private set; }
    public Address Address { get; private set; }

    protected Customer() { }
    
    public Customer(Guid id, string name, string email, string cpf, bool deleted, Address address)
    {
        Id = id;
        Name = name;
        Email = new Email(email);
        Cpf = new CPF(cpf);
        Deleted = deleted;
        Address = address;
    }

    public void ChangeEmail(string email)
    {
        Email = new Email(email);
    }

    public void AddAddress(Address address)
    {
        Address = address;
    }
}