using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class ProductService_Test
{
    private readonly DataContext _context =
    new DataContext(new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateProductAsync_ShouldCreateNewProduct_ThenReturnTrue()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufacturerRepository, productRepository, productPriceRepository);

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

        var productPrice = new ProductPrice
        {
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        var currency = new Currency
        {
            Currency1 = "US DOLLAR",
            Code = "USD"
        };
        await currencyRepository.CreateAsync(currency);

        var productDto = new ProductDto
        {
            ArticleNumber = productEntity.ArticleNumber,
            Title = productEntity.Title,
            Manufacturer = new ManufacturerDto { Manufacture = manufacturer.Manufacture },
            Category = new CategoryDto { CategoryName = category.Categoryname },
            Preamble = productEntity.Preamble,
            Description = productEntity.Description,
            Specification = productEntity.Specification,
            ProductPrice = new ProductPriceDto
            {
                Price = productPrice.Price,
                Currency = new CurrencyDto { Currency1 = currency.Currency1, Code = currency.Code }
            }
        };

        // Act
        var result = await productService.CreateProductAsync(productDto);


        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneProduct_ThenReturnProduct()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufacturerRepository, productRepository, productPriceRepository);

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

        var productPrice = new ProductPrice
        {
            Price = 1234,
            CurrencyCode = "USD"
        };
        await productPriceRepository.CreateAsync(productPrice);

        var currency = new Currency
        {
            Currency1 = "US DOLLAR",
            Code = "USD"
        };
        await currencyRepository.CreateAsync(currency);

        var productDto = new ProductDto
        {
            ArticleNumber = productEntity.ArticleNumber,
            Title = productEntity.Title,
            Manufacturer = new ManufacturerDto { Manufacture = manufacturer.Manufacture },
            Category = new CategoryDto { CategoryName = category.Categoryname },
            Preamble = productEntity.Preamble,
            Description = productEntity.Description,
            Specification = productEntity.Specification,
            ProductPrice = new ProductPriceDto
            {
                Price = productPrice.Price,
                Currency = new CurrencyDto { Currency1 = currency.Currency1, Code = currency.Code }
            }
        };

        // Act
        var result = await productService.GetOneAsync(x => x.ArticleNumber == productDto.ArticleNumber);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.ArticleNumber, result.ArticleNumber);
    }
}
