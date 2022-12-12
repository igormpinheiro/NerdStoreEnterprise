using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Models;
using NSE.Core.Data;

namespace NSE.Cliente.API.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public Task<Customer> GetByCPF(string cpf)
    {
        return _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public async Task<Address> GetAddressById(Guid id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(e => e.CustomerId == id);
    }

    public void AddAddress(Address address)
    {
        _context.Addresses.Add(address);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}