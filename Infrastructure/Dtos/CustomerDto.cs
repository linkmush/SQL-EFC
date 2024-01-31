namespace Infrastructure.Dtos;

public class CustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}
