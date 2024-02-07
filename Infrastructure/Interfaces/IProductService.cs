using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductDto product);
        Task<bool> DeleteAsync(ProductDto product);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetOneAsync(Expression<Func<Product, bool>> predicate);
        Task<ProductDto> UpdateAsync(ProductDto product);
    }
}