using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate);
    }
}