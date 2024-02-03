using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ProductService(CategoryRepository categoryRepository, CurrencyRepository currencyRepository, ManufacturerRepository manufacturerRepository, ProductRepository productRepository, ProductPriceRepository productPriceRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly CurrencyRepository _currencyRepository = currencyRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly ProductPriceRepository _productPriceRepository = productPriceRepository;

    //public async Task<bool> CreateProductAsync();
}
