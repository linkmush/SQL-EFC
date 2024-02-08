using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;                 // Måste lägga till test på update här

public class CurrencyRepository_Test
{
    private readonly DataContext _context =
    new DataContext(new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_Currency_To_CurrencyList()
    {

        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        var currencyEntity = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };

        // ACT
        var result = await currencyRepository.CreateAsync(currencyEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal("USD", result.Code);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllCurrencies()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

        var productPrice = new ProductPrice
        {
            ArticleNumber = 1,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await currencyRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Currency>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Currency_FromList()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

        var productPrice = new ProductPrice
        {
            ArticleNumber = 1,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await currencyRepository.GetOneAsync(x => x.Code == currency.Code);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(currency.Code, result.Code);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_Product()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

        var productPrice = new ProductPrice
        {
            ArticleNumber = 1,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await currencyRepository.DeleteAsync(x => x.Code == currency.Code);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_ProductExists_ThenReturn_Found()
    {
        // ARRANGE
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);

        var currency = new Currency
        {
            Code = "USD",
            Currency1 = "US DOLLAR"
        };
        await currencyRepository.CreateAsync(currency);

        var productPrice = new ProductPrice
        {
            ArticleNumber = 1,
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        // ACT
        var result = await currencyRepository.ExistsAsync(x => x.Code == currency.Code);

        // ASSERT
        Assert.True(result);
    }
}
