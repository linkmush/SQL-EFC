using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CustomerAddressEntity
{
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    public virtual CustomerEntity Customer { get; set; } = null!;
    public virtual AddressEntity Address { get; set; } = null!;
}