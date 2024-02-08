using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;   // måste lägga till delete här

public class ManufacturerRepository_Test
{
    private readonly DataContext _context =
    new DataContext(new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_Manufacturer_To_ManufacturerList()
    {

        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        var manufacturer = new Manufacturer
        {
            Manufacture = "Samsung"
        };

        // ACT
        var result = await manufacturerRepository.CreateAsync(manufacturer);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllManufacturers()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var productEntity = new Product
        {
            ArticleNumber = 1,
            Title = "TestTitle",
            ManufacturerId = 1,
            CategoryId = 1,
            Preamble = "Preamble",
            Description = "Description",
            Specification = "Specification"
        };
        await productRepository.CreateAsync(productEntity);

        // ACT
        var result = await manufacturerRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Manufacturer>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Manufacturer_FromList()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var productEntity = new Product
        {
            ArticleNumber = 1,
            Title = "TestTitle",
            ManufacturerId = 1,
            CategoryId = 1,
            Preamble = "Preamble",
            Description = "Description",
            Specification = "Specification"
        };
        await productRepository.CreateAsync(productEntity);

        // ACT
        var result = await manufacturerRepository.GetOneAsync(x => x.Id == manufacturer.Id);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(manufacturer.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingProduct_ThenReturn_Product()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var productEntity = new Product
        {
            ArticleNumber = 1,
            Title = "TestTitle",
            ManufacturerId = 1,
            CategoryId = 1,
            Preamble = "Preamble",
            Description = "Description",
            Specification = "Specification"
        };
        await productRepository.CreateAsync(productEntity);

        // ACT
        manufacturer.Manufacture = "NewManufacture";
        var result = await manufacturerRepository.UpdateAsync(x => x.Id == manufacturer.Id, manufacturer);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(manufacturer.Id, result.Id);
        Assert.Equal("NewManufacture", result.Manufacture);
    }

    [Fact]
    public async Task Exists_Check_If_ManufacturerExists_ThenReturn_Found()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var productEntity = new Product
        {
            ArticleNumber = 1,
            Title = "TestTitle",
            ManufacturerId = 1,
            CategoryId = 1,
            Preamble = "Preamble",
            Description = "Description",
            Specification = "Specification"
        };
        await productRepository.CreateAsync(productEntity);

        // ACT
        var result = await manufacturerRepository.ExistsAsync(x => x.Id == manufacturer.Id);

        // ASSERT
        Assert.True(result);
    }
}
