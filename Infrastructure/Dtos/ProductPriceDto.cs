namespace Infrastructure.Dtos;

public class ProductPriceDto
{
    public decimal Price { get; set; }
    public CurrencyDto Currency { get; set; } = new CurrencyDto();
}
