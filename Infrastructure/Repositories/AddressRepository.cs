using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AddressRepository(LocalDatabaseContext context) : BaseRepository<AddressEntity>(context)
{
    private readonly LocalDatabaseContext _context = context;
}
