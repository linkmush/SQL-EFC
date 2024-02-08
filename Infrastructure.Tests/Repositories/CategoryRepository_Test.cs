using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;     // Have to add delete test here. 

public class CategoryRepository_Test
{
    private readonly DataContext _context =
    new DataContext(new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_CateGory_To_CategoryList()
    {

        // ARRANGE
        ICategoryRepository categoryRepository = new CategoryRepository(_context);

        var category = new Category
        {
            Categoryname = "CategoryName"
        };

        // ACT
        var result = await categoryRepository.CreateAsync(category);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllCategories()
    {
        // ARRANGE
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

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
        var result = await categoryRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Category_FromList()
    {
        // ARRANGE
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

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
        var result = await categoryRepository.GetOneAsync(x => x.Id == category.Id);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingProduct_ThenReturn_Product()
    {
        // ARRANGE
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

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
        category.Categoryname = "NewCategoryname";
        var result = await categoryRepository.UpdateAsync(x => x.Id == category.Id, category);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        Assert.Equal("NewCategoryname", result.Categoryname);
    }

    [Fact]
    public async Task Exists_Check_If_ProductExists_ThenReturn_Found()
    {
        // ARRANGE
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

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
        var result = await categoryRepository.ExistsAsync(x => x.Id == category.Id);

        // ASSERT
        Assert.True(result);
    }
}
