using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class AddressRepository_Test
{
    private readonly LocalDatabaseContext _context =
    new LocalDatabaseContext(new DbContextOptionsBuilder<LocalDatabaseContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
     .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_Address_To_List()
    {

        // ARRANGE
        IAddressRepository addressRepository = new AddressRepository(_context);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City",
        };

        // ACT
        var result = await addressRepository.CreateAsync(addressEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllAddresses()
    {
        // ARRANGE
        IAddressRepository addressRepository = new AddressRepository(_context);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City",
        };
        await addressRepository.CreateAsync(addressEntity);

        // ACT
        var result = await addressRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Address_FromList()
    {
        // ARRANGE
        IAddressRepository addressRepository = new AddressRepository(_context);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City",
        };
        await addressRepository.CreateAsync(addressEntity);

        // ACT
        var result = await addressRepository.GetOneAsync(x => x.Id == addressEntity.Id);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingAddress_ThenReturn_NewAddress()
    {
        // ARRANGE
        IAddressRepository addressRepository = new AddressRepository(_context);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City",
        };
        await addressRepository.CreateAsync(addressEntity);

        // ACT
        addressEntity.Id = addressEntity.Id;
        addressEntity.StreetName = "NewTestStreetName";
        addressEntity.PostalCode = "34567";
        addressEntity.City = "Stockholm";

        var result = await addressRepository.UpdateAsync(x => x.Id == addressEntity.Id, addressEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(addressEntity.Id, result.Id);
        Assert.Equal("NewTestStreetName", result.StreetName);
        Assert.Equal("34567", result.PostalCode);
        Assert.Equal("Stockholm", result.City);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_Address()
    {
        // ARRANGE
        IAddressRepository addressRepository = new AddressRepository(_context);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City",
        };
        await addressRepository.CreateAsync(addressEntity);

        // ACT
        var result = await addressRepository.DeleteAsync(x => x.Id == addressEntity.Id);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_AddressExists_ThenReturn_Found()
    {
        // ARRANGE
        IAddressRepository addressRepository = new AddressRepository(_context);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City",
        };
        await addressRepository.CreateAsync(addressEntity);

        // ACT
        var result = await addressRepository.ExistsAsync(x => x.Id == addressEntity.Id);

        // ASSERT
        Assert.True(result);
    }

}
