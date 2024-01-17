using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string StreetName { get; set; } = null!;

    [Required]
    [Column(TypeName = "char(6)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    public virtual ICollection<CustomerAddressEntity> CustomerAdress { get; set; } = new HashSet<CustomerAddressEntity>();
}