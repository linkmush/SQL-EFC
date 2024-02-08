using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProductPriceRepository_Test
{
    private readonly DataContext _context =
    new DataContext(new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_ProductPrice_To_ProductPriceList()
    {

        // ARRANGE
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        var productPriceEntity = new ProductPrice
        {
            ArticleNumber = 1,
            CurrencyCode = "SEK"
        };

        // ACT
        var result = await productPriceRepository.CreateAsync(productPriceEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.ArticleNumber);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllProductPrices()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

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

        var productPrice = new ProductPrice
        {
            ArticleNumber = 1,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await productPriceRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductPrice>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Product_FromList()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

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

        var productPrice = new ProductPrice
        {
            ArticleNumber = 1,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await productPriceRepository.GetOneAsync(x => x.ArticleNumber == productPrice.ArticleNumber);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(productPrice.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingProduct_ThenReturn_Product()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

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

        var productPrice = new ProductPrice
        {
            ArticleNumber = productEntity.ArticleNumber,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        productPrice.Price = 5678;
        productPrice.CurrencyCode = "SEK";
        var result = await productPriceRepository.UpdateAsync(x => x.ArticleNumber == productEntity.ArticleNumber, productPrice);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(productPrice.ArticleNumber, result.ArticleNumber);
        Assert.Equal(5678, result.Price);
        Assert.Equal("SEK", result.CurrencyCode);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_ProductPrice()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

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

        var productPrice = new ProductPrice
        {
            ArticleNumber = productEntity.ArticleNumber,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await productPriceRepository.DeleteAsync(x => x.ArticleNumber == productPrice.ArticleNumber);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_ProductExists_ThenReturn_Found()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IProductRepository productRepository = new ProductRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

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

        var productPrice = new ProductPrice
        {
            ArticleNumber = productEntity.ArticleNumber,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await productPriceRepository.ExistsAsync(x => x.ArticleNumber == productPrice.ArticleNumber);

        // ASSERT
        Assert.True(result);
    }
}
