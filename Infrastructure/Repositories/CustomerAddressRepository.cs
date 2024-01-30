using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerAddressRepository(LocalDatabaseContext context) : BaseRepository<CustomerAddressEntity>(context), ICustomerAddressRepository
{
    private readonly LocalDatabaseContext _context = context;

    public override async Task<IEnumerable<CustomerAddressEntity>> GetAllAsync()
    {
        try
        {
            var result = await _context.CustomerAddress.Include(i => i.Address).ToListAsync();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public override async Task<CustomerAddressEntity> GetOneAsync(Expression<Func<CustomerAddressEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.CustomerAddress.Include(i => i.Address).FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
