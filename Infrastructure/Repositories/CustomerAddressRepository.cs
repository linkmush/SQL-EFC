using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerAddressRepository(LocalDatabaseContext context) : BaseRepository<CustomerAddressEntity>(context)
{
    private readonly LocalDatabaseContext _context = context;
}
