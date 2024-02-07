using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<CustomerEntity>
    {
    }
}