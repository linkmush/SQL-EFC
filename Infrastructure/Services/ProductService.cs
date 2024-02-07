using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ProductService(ICategoryRepository categoryRepository, ICurrencyRepository currencyRepository, IManufacturerRepository manufacturerRepository, IProductRepository productRepository, IProductPriceRepository productPriceRepository) : IProductService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductPriceRepository _productPriceRepository = productPriceRepository;

    public async Task<bool> CreateProductAsync(ProductDto product)
    {
        try
        {
            if (!await _productRepository.ExistsAsync(x => x.ArticleNumber == product.ArticleNumber))
            {
                var manufacturerEntity = await _manufacturerRepository.GetOneAsync(x => x.Manufacture == product.Manufacturer.Manufacture);
                if (manufacturerEntity == null)
                {
                    manufacturerEntity = await _manufacturerRepository.CreateAsync(new Manufacturer { Manufacture = product.Manufacturer.Manufacture });
                }

                var categoryEntity = await _categoryRepository.GetOneAsync(x => x.Categoryname == product.Category.CategoryName);
                if (categoryEntity == null)
                {
                    categoryEntity = await _categoryRepository.CreateAsync(new Category { Categoryname = product.Category.CategoryName });
                }

                var currencyEntity = await _currencyRepository.GetOneAsync(x => x.Code == product.ProductPrice.Currency.Code);
                if (currencyEntity == null)
                {
                    currencyEntity = await _currencyRepository.CreateAsync(new Currency
                    {
                        Code = product.ProductPrice.Currency.Code,
                        Currency1 = product.ProductPrice.Currency.Currency1,
                    });
                }

                var productEntity = new Product
                {
                    Title = product.Title,
                    Preamble = product.Preamble,
                    Description = product.Description,
                    Specification = product.Specification,
                    Manufacturer = manufacturerEntity,
                    Category = categoryEntity,
                    ManufacturerId = manufacturerEntity.Id,
                    CategoryId = categoryEntity.Id,
                    ProductPrice = new ProductPrice
                    {
                        Price = product.ProductPrice.Price,
                        CurrencyCodeNavigation = currencyEntity,
                    },
                };

                await _productRepository.CreateAsync(productEntity);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }

    public async Task<ProductDto> GetOneAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var productEntity = await _productRepository.GetOneAsync(predicate);

            if (productEntity != null)
            {

                var productDto = new ProductDto
                {
                    ArticleNumber = productEntity.ArticleNumber,
                    Title = productEntity.Title,
                    ManufacturerId = productEntity.ManufacturerId,
                    CategoryId = productEntity.CategoryId,
                    Preamble = productEntity.Preamble,
                    Description = productEntity.Description,
                    Specification = productEntity.Specification,
                    Category = new CategoryDto
                    {
                        Id = productEntity.Category.Id,
                        CategoryName = productEntity.Category.Categoryname,
                    },
                    Manufacturer = new ManufacturerDto
                    {
                        Id = productEntity.Manufacturer.Id,
                        Manufacture = productEntity.Manufacturer.Manufacture,
                    },
                };

                if (productEntity.ProductPrice != null)
                {
                    productDto.ProductPrice = new ProductPriceDto
                    {
                        Price = productEntity.ProductPrice.Price,
                        Currency = new CurrencyDto
                        {
                            Currency1 = productEntity.ProductPrice.CurrencyCodeNavigation.Currency1,
                            Code = productEntity.ProductPrice.CurrencyCode
                        }
                    };
                }
                return productDto;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = new List<ProductDto>();

        try
        {
            var productEnteties = await _productRepository.GetAllAsync();

            foreach (var item in productEnteties)
            {
                var productDto = new ProductDto
                {
                    ArticleNumber = item.ArticleNumber,
                    Title = item.Title,
                    ManufacturerId = item.Manufacturer.Id,
                    CategoryId = item.CategoryId,
                    Preamble = item.Preamble,
                    Description = item.Description,
                    Specification = item.Specification,
                    Category = new CategoryDto
                    {
                        Id = item.Category.Id,
                        CategoryName = item.Category.Categoryname,
                    },
                    Manufacturer = new ManufacturerDto
                    {
                        Id = item.Manufacturer.Id,
                        Manufacture = item.Manufacturer.Manufacture,
                    },
                };

                if (item.ProductPrice != null)
                {
                    productDto.ProductPrice = new ProductPriceDto
                    {
                        Price = item.ProductPrice.Price,
                        Currency = new CurrencyDto
                        {
                            Currency1 = item.ProductPrice.CurrencyCodeNavigation.Currency1,
                            Code = item.ProductPrice.CurrencyCode
                        }
                    };

                    products.Add(productDto);
                }
            }

            return products;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<ProductDto> UpdateAsync(ProductDto product)
    {
        try
        {
            var updateProduct = await _productRepository.GetOneAsync(x => x.ArticleNumber == product.ArticleNumber);
            if (updateProduct != null)
            {
                updateProduct.Title = product.Title;
                updateProduct.Preamble = product.Preamble;
                updateProduct.Description = product.Description;
                updateProduct.Specification = product.Specification;

                if (product.Category != null)
                {
                    var categoryExists = await _categoryRepository.ExistsAsync(x => x.Categoryname == product.Category.CategoryName);

                    if (categoryExists)
                    {
                        var existingCategory = await _categoryRepository.GetOneAsync(c => c.Categoryname == product.Category.CategoryName);
                        updateProduct.Category = existingCategory;
                    }
                    else
                    {
                        updateProduct.Category = new Category { Categoryname = product.Category.CategoryName };
                    }
                }

                if (product.Manufacturer != null)
                {
                    var manufacturerExists = await _manufacturerRepository.ExistsAsync(x => x.Manufacture == product.Manufacturer.Manufacture);

                    if (manufacturerExists)
                    {
                        var existingManufacturer = await _manufacturerRepository.GetOneAsync(c => c.Manufacture == product.Manufacturer.Manufacture);
                        updateProduct.Manufacturer = existingManufacturer;
                    }
                    else
                    {
                        updateProduct.Manufacturer = new Manufacturer { Manufacture = product.Manufacturer.Manufacture };
                    }
                }


                if (updateProduct.ProductPrice != null)
                {
                    updateProduct.ProductPrice.Price = product.ProductPrice.Price;

                    if (product.ProductPrice.Currency.Code != null)
                    {
                        var currencyExists = await _currencyRepository.ExistsAsync(c => c.Code == product.ProductPrice.Currency.Code);

                        if (currencyExists)
                        {
                            var existingCurrency = await _currencyRepository.GetOneAsync(c => c.Code == product.ProductPrice.Currency.Code);
                            updateProduct.ProductPrice.CurrencyCodeNavigation = existingCurrency;
                        }
                        else
                        {
                            updateProduct.ProductPrice.CurrencyCodeNavigation = new Currency
                            {
                                Code = product.ProductPrice.Currency.Code,
                                Currency1 = product.ProductPrice.Currency.Currency1
                            };
                        }
                    }
                }


                var updatedCustomerEntity = await _productRepository.UpdateAsync(x => x.ArticleNumber == updateProduct.ArticleNumber, updateProduct);

                return product;

            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(ProductDto product)
    {
        try
        {
            var productEntity = await _productRepository.GetOneAsync(x => x.ArticleNumber == product.ArticleNumber);

            if (productEntity != null)
            {
                await _productPriceRepository.DeleteAsync(x => x.ArticleNumber == productEntity.ArticleNumber);
                await _productRepository.DeleteAsync(x => x.ArticleNumber == productEntity.ArticleNumber);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }
}
