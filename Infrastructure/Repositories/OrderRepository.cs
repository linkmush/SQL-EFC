using Infrastructure.Context;
using Infrastructure.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class OrderRepository(LocalDatabaseContext context)
{
    private readonly LocalDatabaseContext _context = context;

    // Create 
    public bool Create(OrderEntity entity)
    {
        try
        {
            _context.Add(entity);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


    // Read 
    public IEnumerable<OrderEntity> GetAll()
    {
        try
        {
            var orders = _context.Orders.ToList();
            if (orders != null)
            return orders;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public OrderEntity GetOne(Expression<Func<OrderEntity, bool>> predicate)
    {
        try
        {
            var entity = _context.Orders.FirstOrDefault(predicate);

            if (entity != null)
                return entity;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


    // Update 
    public OrderEntity Update(OrderEntity entity)
    {
        try
        {
            var entityUpdate = _context.Orders.FirstOrDefault(x => x.Id == entity.Id);

            if (entityUpdate != null)
            {
                entityUpdate.CustomerId = entityUpdate.CustomerId;
                _context.Orders.Update(entityUpdate);
                _context.SaveChanges();
                return entity;
            }
            else
            {
                Console.WriteLine("Entity not found");
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    // Delete
    public bool Delete(int id)
    {
        try
        {
            var entity = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (entity != null)
                _context.Remove(entity);
                _context.SaveChanges();
                return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
