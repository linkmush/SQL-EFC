using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CustomerInfoRepository(LocalDatabaseContext context) : BaseRepository<CustomerInfoEntity, LocalDatabaseContext>(context), ICustomerInfoRepository
{
    private readonly LocalDatabaseContext _context = context;
}
