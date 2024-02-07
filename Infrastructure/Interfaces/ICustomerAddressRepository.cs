using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface ICustomerAddressRepository : IBaseRepository<CustomerAddressEntity>
    {
    }
}