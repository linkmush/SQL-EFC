using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class CustomerRepository_Test
{
    private readonly LocalDatabaseContext _context =
    new LocalDatabaseContext(new DbContextOptionsBuilder<LocalDatabaseContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_Customer_To_List()
    {

        // ARRANGE
        ICustomerRepository customerRepository = new CustomerRepository(_context);

        var customerEntity = new CustomerEntity
        {
            Email = "TestEmail"
        };

        // ACT
        var result = await customerRepository.CreateAsync(customerEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal("TestEmail", result.Email);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllCustomers()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };
        await orderRepository.CreateAsync(orderEntity);

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
        var result = await customerRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Customer_FromList()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };
        await orderRepository.CreateAsync(orderEntity);

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
        var result = await customerRepository.GetOneAsync(x => x.Id == customerEntity.Id);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(orderEntity.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingCustomer_ThenReturn_NewCustomer()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };
        await orderRepository.CreateAsync(orderEntity);

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
        customerEntity.Id = customerEntity.Id;
        customerEntity.Email = "NewTestEmail";

        var result = await customerRepository.UpdateAsync(x => x.Id == customerEntity.Id, customerEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(customerEntity.Id, result.Id);
        Assert.Equal("NewTestEmail", result.Email);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_Customer()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };
        await orderRepository.CreateAsync(orderEntity);

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
        var result = await customerRepository.DeleteAsync(x => x.Id == customerEntity.Id);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_CustomerExists_ThenReturn_Found()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };
        await orderRepository.CreateAsync(orderEntity);

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
        var result = await customerRepository.ExistsAsync(x => x.Id == customerEntity.Id);

        // ASSERT
        Assert.True(result);
    }
}
