using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProductRepository_Test
{
    private readonly DataContext _context = 
        new DataContext(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_Product_To_ProductList()
    {

        // ARRANGE
        IProductRepository productRepository = new ProductRepository(_context);
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

        // ACT
        var result = await productRepository.CreateAsync(productEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.ArticleNumber);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllProducts()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var category = new Category
        {
            Categoryname = "Categoryname",
        };
        await categoryRepository.CreateAsync(category);

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
        var result = await productRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Product_FromList()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var category = new Category
        {
            Categoryname = "Categoryname",
        };
        await categoryRepository.CreateAsync(category);

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
        var result = await productRepository.GetOneAsync(x => x.ArticleNumber == productEntity.ArticleNumber);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(productEntity.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingProduct_ThenReturn_Product()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var category = new Category
        {
            Categoryname = "Categoryname",
        };
        await categoryRepository.CreateAsync(category);

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
        productEntity.Title = "NewTitle";
        productEntity.Preamble = "Preamble";
        productEntity.Description = "NewDescription";
        productEntity.Specification = "NewSpecification";
        var result = await productRepository.UpdateAsync(x => x.ArticleNumber == productEntity.ArticleNumber, productEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(productEntity.ArticleNumber, result.ArticleNumber);
        Assert.Equal("NewTitle", result.Title);
        Assert.Equal("Preamble", result.Preamble);
        Assert.Equal("NewDescription", result.Description);
        Assert.Equal("NewSpecification", result.Specification);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_Product()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var category = new Category
        {
            Categoryname = "Categoryname",
        };
        await categoryRepository.CreateAsync(category);

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
        var result = await productRepository.DeleteAsync(x => x.ArticleNumber == productEntity.ArticleNumber);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_ProductExists_ThenReturn_Found()
    {
        // ARRANGE
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var manufacturer = new Manufacturer
        {
            Manufacture = "Manufacture",
        };
        await manufacturerRepository.CreateAsync(manufacturer);

        var category = new Category
        {
            Categoryname = "Categoryname",
        };
        await categoryRepository.CreateAsync(category);

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
        var result = await productRepository.ExistsAsync(x => x.ArticleNumber == productEntity.ArticleNumber);

        // ASSERT
        Assert.True(result);
    }
}
