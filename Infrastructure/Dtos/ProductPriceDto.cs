namespace Infrastructure.Dtos;

public class ProductPriceDto
{
    public decimal Price { get; set; }
    public string CurrencyCode { get; set; } = null!;
    public CurrencyDto Currency { get; set; } = new CurrencyDto();
}
