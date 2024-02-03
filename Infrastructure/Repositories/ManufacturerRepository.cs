using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ManufacturerRepository(DataContext context) : BaseRepository<Manufacturer, DataContext>(context), IManufacturerRepository
{
    private readonly DataContext _context = context;
}