using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CategoryRepository(DataContext context) : BaseRepository<Category, DataContext>(context), ICategoryRepository
{
    private readonly DataContext _context = context;
}