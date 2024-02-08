using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class OrderRepository_Test
{
    private readonly LocalDatabaseContext _context =
    new LocalDatabaseContext(new DbContextOptionsBuilder<LocalDatabaseContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task CreateAsync_Should_Add_One_CustomerId_To_OrderList()
    {

        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };

        // ACT
        var result = await orderRepository.CreateAsync(orderEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.CustomerId);
    }

    [Fact]
    public async Task GetAsync_ShouldGetAllRecords_ReturnAllOrders()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };

        await orderRepository.CreateAsync(orderEntity);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await orderRepository.GetAllAsync();

        // ASSERT
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<OrderEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_GetOne_Product_FromList()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };

        await orderRepository.CreateAsync(orderEntity);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await orderRepository.GetOneAsync(x => x.CustomerId == orderEntity.CustomerId);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(orderEntity.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_ExistingOrder_ThenReturn_NewOrder()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };

        await orderRepository.CreateAsync(orderEntity);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        orderEntity.CustomerId = customerEntity.Id;

        var result = await orderRepository.UpdateAsync(x => x.CustomerId == orderEntity.CustomerId, orderEntity);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(orderEntity.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOne_Order_FromList()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };

        await orderRepository.CreateAsync(orderEntity);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await orderRepository.DeleteAsync(x => x.CustomerId == orderEntity.CustomerId);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task Exists_Check_If_OrderExists_ThenReturn_Found()
    {
        // ARRANGE
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerRepository customerRepository = new CustomerRepository(_context);


        var orderEntity = new OrderEntity
        {
            CustomerId = 1
        };

        await orderRepository.CreateAsync(orderEntity);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "Oskar@Domain.com"
        };
        await customerRepository.CreateAsync(customerEntity);

        // ACT
        var result = await orderRepository.ExistsAsync(x => x.CustomerId == orderEntity.CustomerId);

        // ASSERT
        Assert.True(result);
    }
}
