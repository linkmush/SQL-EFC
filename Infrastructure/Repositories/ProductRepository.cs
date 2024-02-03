using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductRepository(DataContext context) : BaseRepository<Product, DataContext>(context), IProductRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var result = await _context.Products.Include(i => i.Manufacturer).Include(i => i.Category).Include(i => i.ProductPrice).ThenInclude(i => i.CurrencyCodeNavigation).ToListAsync();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public override async Task<Product> GetOneAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var result = await _context.Products.Include(i => i.Manufacturer).Include(i => i.Category).Include(i => i.ProductPrice).ThenInclude(i => i.CurrencyCodeNavigation).FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
