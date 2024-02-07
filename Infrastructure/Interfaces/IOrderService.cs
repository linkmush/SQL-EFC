using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<bool> CreateCustomerAsync(CustomerRegistrationDto customer);
    Task<bool> DeleteAsync(CustomerDto customer);
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate);
    Task<CustomerDto> UpdateAsync(CustomerDto customer);
}