using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;
public class AddressRepository(LocalDatabaseContext context) : BaseRepository<AddressEntity, LocalDatabaseContext>(context), IAddressRepository
{
    private readonly LocalDatabaseContext _context = context;
}
