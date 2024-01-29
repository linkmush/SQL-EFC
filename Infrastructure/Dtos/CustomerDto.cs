namespace Infrastructure.Dtos;

public class CustomerDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? CustomerId { get; set; }
    public int AddressId { get; set; }

    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}
