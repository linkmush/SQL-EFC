using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ProductPriceRepository(DataContext context) : BaseRepository<ProductPrice, DataContext>(context), IProductPriceRepository
{
    private readonly DataContext _context = context;
}
