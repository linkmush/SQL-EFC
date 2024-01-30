using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface ICustomerAddressRepository
    {
        Task<IEnumerable<CustomerAddressEntity>> GetAllAsync();
        Task<CustomerAddressEntity> GetOneAsync(Expression<Func<CustomerAddressEntity, bool>> predicate);
    }
}