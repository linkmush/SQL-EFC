using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations.Schema;
namespace Infrastructure.Entities;

public class CustomerAddressEntity
{
    [ForeignKey(nameof(Customer))]
    public int? CustomerId { get; set; }

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    public virtual CustomerEntity Customer { get; set; } = null!;
    public virtual AddressEntity Address { get; set; } = null!;

    public static implicit operator CustomerAddressEntity(CustomerDto customer)
    {
        var customerAddressEntity = new CustomerAddressEntity
        {
            CustomerId = customer.CustomerId,
            AddressId = customer.AddressId,
        };

        return customerAddressEntity;
    }
}