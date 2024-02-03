namespace Infrastructure.Dtos;

public class ProductDto
{
    public int ArticleNumber { get; set; }
    public string Title { get; set; } = null!;
    public int ManufacturerId { get; set; }
    public int CategoryId { get; set; }
    public string? Preamble { get; set; }
    public string? Description { get; set; }
    public string? Specification { get; set; }

    public ProductPriceDto ProductPrice { get; set; } = new ProductPriceDto();
    public ManufacturerDto Manufacturer { get; set; } = new ManufacturerDto();
    public CategoryDto Category { get; set; } = new CategoryDto();
    public List<CurrencyDto> Currencies { get; set; } = new List<CurrencyDto>();
}