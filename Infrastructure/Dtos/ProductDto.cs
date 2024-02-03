namespace Infrastructure.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int ManufacturerId { get; set; }
    public int CategoryId { get; set; }
    public string Preamble { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Specification { get; set; } = null!;

    public ManufacturerDto Manufacturer { get; set; } = new ManufacturerDto();

    public CategoryDto Category { get; set; } = new CategoryDto();
}