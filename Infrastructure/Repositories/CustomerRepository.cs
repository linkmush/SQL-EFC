using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(LocalDatabaseContext context) : BaseRepository<CustomerEntity>(context)
{
    private readonly LocalDatabaseContext _context = context;

    

    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            var result = await _context.Customers.Include(i => i.CustomerInfo).Include(i => i.Orders).Include(i => i.CustomerAddress).ThenInclude(i => i.Address).ToListAsync();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public override async Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Customers.Include(i => i.CustomerInfo).Include(i => i.Orders).Include(i => i.CustomerAddress).ThenInclude(i => i.Address).FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
