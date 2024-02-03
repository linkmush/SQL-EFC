using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ProductService(CategoryRepository categoryRepository, CurrencyRepository currencyRepository, ManufacturerRepository manufacturerRepository, ProductRepository productRepository, ProductPriceRepository productPriceRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly CurrencyRepository _currencyRepository = currencyRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly ProductPriceRepository _productPriceRepository = productPriceRepository;

    public async Task<bool> CreateProductAsync(ProductDto product)
    {
        try
        {
            if (!await _productRepository.ExistsAsync(x => x.ArticleNumber == product.ArticleNumber))
            {
                var productEntity = await _productRepository.GetOneAsync(x => x.ArticleNumber == product.ArticleNumber);
                if (productEntity != null)
                {
                    productEntity = new Product
                    {
                        Title = product.Title,
                        ManufacturerId = product.ManufacturerId,
                        CategoryId = product.CategoryId,
                        Preamble = product.Preamble,
                        Description = product.Description,
                        Specification = product.Specification,
                        Category = new Category
                        {
                            Categoryname = product.Category.CategoryName,
                        },
                        Manufacturer = new Manufacturer
                        {
                            Manufacture = product.Manufacturer.Manufacture
                        },
                        ProductPrice = new ProductPrice
                        {
                            Price = product.ProductPrice.Price,
                            CurrencyCodeNavigation = new Currency
                            {
                                Code = product.ProductPrice.Currency.Code,
                            }
                        }
                    };
                    await _productRepository.CreateAsync(productEntity);

                    return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
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
                            Currency1 = productEntity.ProductPrice.CurrencyCode,
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
                            Currency1 = item.ProductPrice.CurrencyCode,
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
                if (updateProduct.ArticleNumber != product.ArticleNumber)
                {
                    if (!await _productRepository.ExistsAsync(x => x.ArticleNumber == product.ArticleNumber))
                        updateProduct.ArticleNumber = product.ArticleNumber;
                }
                updateProduct.Title = product.Title;
                updateProduct.Preamble = product.Preamble;
                updateProduct.Description = product.Description;
                updateProduct.Category.Categoryname = product.Category.CategoryName;
                updateProduct.Manufacturer.Manufacture = product.Manufacturer.Manufacture;


                if (updateProduct.ProductPrice != null)
                {
                    updateProduct.ProductPrice.Price = product.ProductPrice.Price;

                    if (updateProduct.ProductPrice.CurrencyCodeNavigation != null)
                    {
                        updateProduct.ProductPrice.CurrencyCodeNavigation.Code = product.ProductPrice.Currency.Code;
                        updateProduct.ProductPrice.CurrencyCodeNavigation.Currency1 = product.ProductPrice.Currency.Currency1;
                    }
                }


                var updatedCustomerEntity = await _productRepository.UpdateAsync(x => x.ArticleNumber == updateProduct.ArticleNumber, updateProduct);

                return product;

            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
