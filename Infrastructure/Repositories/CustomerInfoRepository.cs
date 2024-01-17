using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerInfoRepository(LocalDatabaseContext context) : BaseRepository<CustomerInfoEntity>(context)
{
    private readonly LocalDatabaseContext _context = context;
}
