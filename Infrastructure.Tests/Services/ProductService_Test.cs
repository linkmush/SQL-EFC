using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;     // Måste lägga till GetAll 
  
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

    [Fact]
    public async Task GetProductsAsync_Should_GetAll_And_ReturnIEnumerableOfTypeProductDto()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_context);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_context);
        IManufacturerRepository manufacturerRepository = new ManufacturerRepository(_context);
        ICategoryRepository categoryRepository = new CategoryRepository(_context);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_context);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufacturerRepository, productRepository, productPriceRepository);

        var productDto = new ProductDto
        {
            ArticleNumber = 1,
            Title = "Test",
            Description = "Test",
            Specification = "Test",
            Manufacturer = new ManufacturerDto { Id = 1, Manufacture = "Test" },
            Category = new CategoryDto { Id = 1, CategoryName = "Test" },
            ProductPrice = new ProductPriceDto
            {
                Price = 100,
                Currency = new CurrencyDto { Code = "Test", Currency1 = "Test" }
            }
        };
        await productService.CreateProductAsync(productDto);

        // Act
        var result = await productService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
        Assert.Single(result);
    }


    [Fact]
    public async Task UpdateProductAsync_Should_UpdateExistingProduct_And_ReturnUpdatedProductDto()
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
        await productService.CreateProductAsync(productDto);

        // Act
        productDto.Title = "NewTitle";
        productDto.Description = "NewDescription";
        productDto.Specification = "NewSpecification";
        productDto.ManufacturerId = 2;
        productDto.CategoryId = 2;
        productDto.ProductPrice.Price = 200;
        productDto.ProductPrice.CurrencyCode = "NewCurrencyCode";
        productDto.ProductPrice.Currency.Code = "NewCurrencyCode";
        productDto.ProductPrice.Currency.Currency1 = "NewCurrency1";

        var result = await productService.UpdateAsync(productDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.ArticleNumber, result.ArticleNumber);
        Assert.Equal("NewTitle", result.Title);
        Assert.Equal("NewDescription", result.Description);
        Assert.Equal("NewSpecification", result.Specification);
        Assert.Equal(2, result.ManufacturerId);
        Assert.Equal(2, result.CategoryId);
        Assert.Equal(200, result.ProductPrice.Price);
        Assert.Equal("NewCurrencyCode", result.ProductPrice.CurrencyCode);
        Assert.Equal("NewCurrencyCode", result.ProductPrice.Currency.Code);
        Assert.Equal("NewCurrency1", result.ProductPrice.Currency.Currency1);
    }

    [Fact]
    public async Task DeleteProductAsync_Should_RemoveOneProduct_And_ReturnTrue()
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
        await productService.CreateProductAsync(productDto);

        // Act
        var result = await productService.DeleteAsync(productDto);

        // Assert
        Assert.True(result);
    }
}
