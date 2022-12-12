using NSE.Core.Data;

namespace NSE.Cliente.API.Models;

public interface ICustomerRepository : IRepository<Customer>
{
    void Add(Customer customer);

    Task<IEnumerable<Customer>> GetAll();

    Task<Customer> GetByCPF(string cpf);

    void AddAddress(Address address);

    Task<Address> GetAddressById(Guid id);
}