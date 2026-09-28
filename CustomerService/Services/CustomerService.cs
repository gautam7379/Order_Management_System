using CustomerService.Models;
using CustomerService.Repositories;

namespace CustomerService.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        return await _repository.CreateAsync(customer);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        return await _repository.UpdateAsync(customer);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<List<Customer>> SearchByCityAsync(string city)
    {
        return await _repository.SearchByCityAsync(city);
    }
}