using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CurrencyRepository(DataContext context) : BaseRepository<Currency, DataContext>(context), ICurrencyRepository
{
    private readonly DataContext _context = context;
}
