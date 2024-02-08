using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class CustomerInfoRepository_Test
{
    private readonly LocalDatabaseContext _context =
    new LocalDatabaseContext(new DbContextOptionsBuilder<LocalDatabaseContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_CustomerInfo_To_List()
    {

        // ARRANGE
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var customerInfoEntity = new CustomerInfoEntity
        {
            CustomerId = 1,
            FirstName = "Test",
            LastName = "Test",
        };

        // ACT
        var result = await customerInfoRepository.CreateAsync(customerInfoEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.CustomerId);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllCustomerInfos()
    {
        // ARRANGE
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var customerInfo = new CustomerInfoEntity
        {
            CustomerId = 1,
            FirstName = "FirstName",
            LastName = "LastName"
        };
        await customerInfoRepository.CreateAsync(customerInfo);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await customerInfoRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerInfoEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_CustomerInfo_FromList()
    {
        // ARRANGE
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var customerInfo = new CustomerInfoEntity
        {
            CustomerId = 1,
            FirstName = "FirstName",
            LastName = "LastName"
        };
        await customerInfoRepository.CreateAsync(customerInfo);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await customerInfoRepository.GetOneAsync(x => x.CustomerId == customerInfo.CustomerId);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(customerInfo.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingCustomerInfo_ThenReturn_NewCustomerInfo()
    {
        // ARRANGE
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var customerInfo = new CustomerInfoEntity
        {
            CustomerId = 1,
            FirstName = "FirstName",
            LastName = "LastName"
        };
        await customerInfoRepository.CreateAsync(customerInfo);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        customerInfo.CustomerId = customerEntity.Id;
        customerInfo.FirstName = "NewTestFirstName";
        customerInfo.LastName = "NewTestLastName";

        var result = await customerInfoRepository.UpdateAsync(x => x.CustomerId == customerInfo.CustomerId, customerInfo);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(customerInfo.CustomerId, result.CustomerId);
        Assert.Equal("NewTestFirstName", result.FirstName);
        Assert.Equal("NewTestLastName", result.LastName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_CustomerInfo()
    {
        // ARRANGE
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var customerInfo = new CustomerInfoEntity
        {
            CustomerId = 1,
            FirstName = "FirstName",
            LastName = "LastName"
        };
        await customerInfoRepository.CreateAsync(customerInfo);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await customerInfoRepository.DeleteAsync(x => x.CustomerId == customerInfo.CustomerId);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_CustomerInfoExists_ThenReturn_Found()
    {
        // ARRANGE
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var customerInfo = new CustomerInfoEntity
        {
            CustomerId = 1,
            FirstName = "FirstName",
            LastName = "LastName"
        };
        await customerInfoRepository.CreateAsync(customerInfo);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await customerInfoRepository.ExistsAsync(x => x.CustomerId == customerInfo.CustomerId);
   
        // ASSERT
        Assert.True(result);
    }
}
