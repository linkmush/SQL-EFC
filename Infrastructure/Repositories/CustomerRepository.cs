using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerRepository(LocalDatabaseContext context) : BaseRepository<CustomerEntity>(context)
{
    private readonly LocalDatabaseContext _context = context;
}
