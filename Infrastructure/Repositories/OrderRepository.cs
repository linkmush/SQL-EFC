using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRepository(LocalDatabaseContext context) : BaseRepository<OrderEntity>(context), IOrderRepository
{
    private readonly LocalDatabaseContext _context = context;
}
