using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class OrderService_Test
{
    private readonly LocalDatabaseContext _context =
    new LocalDatabaseContext(new DbContextOptionsBuilder<LocalDatabaseContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    [Fact]
    public async Task CreateCustomerAsync_ShouldCreateNewCustomer_ThenReturnTrue()
    {
        // Arrange
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerAddressRepository customerAddressRepository = new CustomerAddressRepository(_context);
        IAddressRepository addressRepository = new AddressRepository(_context);
        IOrderService orderService = new OrderService(orderRepository, customerRepository, customerInfoRepository, customerAddressRepository, addressRepository);

        var customerDto = new CustomerRegistrationDto
        {
            Email = "test",
            FirstName = "test",
            LastName = "test",
            StreetName = "test",
            PostalCode = "test",
            City = "test"
        };


        // Act
        var result = await orderService.CreateCustomerAsync(customerDto);


        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneCustomer_ThenReturnCustomer()
    {
        // Arrange
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerAddressRepository customerAddressRepository = new CustomerAddressRepository(_context);
        IAddressRepository addressRepository = new AddressRepository(_context);
        IOrderService orderService = new OrderService(orderRepository, customerRepository, customerInfoRepository, customerAddressRepository, addressRepository);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "test",
            CustomerInfo = new CustomerInfoEntity
            {
                FirstName = "test",
                LastName = "test",
            },
        };
        await customerRepository.CreateAsync(customerEntity);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "test",
            PostalCode = "test",
            City = "test"
        };
        await addressRepository.CreateAsync(addressEntity);

        var customerAdress = new CustomerAddressEntity
        {
            CustomerId = 1,
            AddressId = 1,
        };
        await customerAddressRepository.CreateAsync(customerAdress);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1,
        };
        await orderRepository.CreateAsync(orderEntity);

        var customerRegistrationDto = new CustomerRegistrationDto
        {
            Email = "test",
            FirstName = "test",
            LastName = "test",
            StreetName= "test",
            PostalCode= "test",
            City= "test",
        };

        // Act
        var result = await orderService.GetOneAsync(x => x.Email == customerEntity.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerEntity.Email, result.Email);
    }

    [Fact]
    public async Task GetUsersAsync_Should_GetAllCustomers_ThenReturnList()
    {
        // Arrange
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerAddressRepository customerAddressRepository = new CustomerAddressRepository(_context);
        IAddressRepository addressRepository = new AddressRepository(_context);
        IOrderService orderService = new OrderService(orderRepository, customerRepository, customerInfoRepository, customerAddressRepository, addressRepository);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "test",
            CustomerInfo = new CustomerInfoEntity
            {
                FirstName = "test",
                LastName = "test",
            },
        };
        await customerRepository.CreateAsync(customerEntity);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "test",
            PostalCode = "test",
            City = "test"
        };
        await addressRepository.CreateAsync(addressEntity);

        var customerAdress = new CustomerAddressEntity
        {
            CustomerId = 1,
            AddressId = 1,
        };
        await customerAddressRepository.CreateAsync(customerAdress);

        var orderEntity = new OrderEntity
        {
            CustomerId = 1,
        };
        await orderRepository.CreateAsync(orderEntity);

        // Act
        var result = await orderService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task UpdateCustomerAsync_Should_UpdateExistingCustomer_And_ReturnUpdatedCustomerDto()
    {
        // Arrange
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerAddressRepository customerAddressRepository = new CustomerAddressRepository(_context);
        IAddressRepository addressRepository = new AddressRepository(_context);
        IOrderService orderService = new OrderService(orderRepository, customerRepository, customerInfoRepository, customerAddressRepository, addressRepository);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "test",
            CustomerInfo = new CustomerInfoEntity
            {
                FirstName = "test",
                LastName = "test",
            },
        };
        await customerRepository.CreateAsync(customerEntity);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "test",
            PostalCode = "test",
            City = "test"
        };
        await addressRepository.CreateAsync(addressEntity);

        var customerAddress = new CustomerAddressEntity
        {
            CustomerId = 1,
            AddressId = 1,
        };
        await customerAddressRepository.CreateAsync(customerAddress);

        var updatedCustomerDto = new CustomerDto
        {
            Id = 1,
            Email = "newtest@test.com",
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            Addresses = new List<AddressDto>
        {
            new AddressDto
            {
                Id = 1,
                StreetName = "NewStreet",
                PostalCode = "NewPostalCode",
                City = "NewCity"
            }
        }
        };

        // Act
        updatedCustomerDto.Id = 1;
        updatedCustomerDto.Email = "newtest@test.com";
        updatedCustomerDto.FirstName = "NewFirstName";
        updatedCustomerDto.LastName = "NewLastName";
        updatedCustomerDto.Addresses[0].StreetName = "NewStreet";
        updatedCustomerDto.Addresses[0].PostalCode = "NewPostalCode";
        updatedCustomerDto.Addresses[0].City = "NewCity";

        var result = await orderService.UpdateAsync(updatedCustomerDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedCustomerDto.Id, result.Id);
        Assert.Equal("newtest@test.com", result.Email);
        Assert.Equal("NewFirstName", result.FirstName);
        Assert.Equal("NewLastName", result.LastName);
        Assert.Equal("NewStreet", result.Addresses[0].StreetName);
        Assert.Equal("NewPostalCode", result.Addresses[0].PostalCode);
        Assert.Equal("NewCity", result.Addresses[0].City);

    }

    [Fact]
    public async Task DeleteCustomerAsync_Should_RemoveOneCustomer_And_ReturnTrue()
    {
        // Arrange
        ICustomerRepository customerRepository = new CustomerRepository(_context);
        ICustomerInfoRepository customerInfoRepository = new CustomerInfoRepository(_context);
        IOrderRepository orderRepository = new OrderRepository(_context);
        ICustomerAddressRepository customerAddressRepository = new CustomerAddressRepository(_context);
        IAddressRepository addressRepository = new AddressRepository(_context);
        IOrderService orderService = new OrderService(orderRepository, customerRepository, customerInfoRepository, customerAddressRepository, addressRepository);

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            Email = "test",
            CustomerInfo = new CustomerInfoEntity
            {
                FirstName = "test",
                LastName = "test",
            },
        };
        await customerRepository.CreateAsync(customerEntity);

        var addressEntity = new AddressEntity
        {
            Id = 1,
            StreetName = "test",
            PostalCode = "test",
            City = "test"
        };
        await addressRepository.CreateAsync(addressEntity);

        var customerAddress = new CustomerAddressEntity
        {
            CustomerId = 1,
            AddressId = 1,
        };
        await customerAddressRepository.CreateAsync(customerAddress);

        // Act
        var result = await orderService.DeleteAsync(new CustomerDto { Email = "test" });

        // Assert
        Assert.True(result);
    }
}
