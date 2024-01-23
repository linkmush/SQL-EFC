using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<LocalDatabaseContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\SQL-EFC\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30", x => x.MigrationsAssembly(nameof(Infrastructure))));

    services.AddScoped<OrderRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<CustomerInfoRepository>();
    services.AddScoped<CustomerAddressRepository>();
    services.AddScoped<AddressRepository>();
    services.AddScoped<OrderService>();

}).Build();

builder.Start();

Console.Clear();
var orderService = builder.Services.GetRequiredService<OrderService>();
var result = orderService.CreateCustomer(new CreateCustomerDto
{
    FirstName = "Oskar",
    LastName = "Lindqvist",
    Email = "oskar@domain.com",
    StreetName = "hejhejgatan 12",
    PostalCode = "12345",
    City = "Bangkok"
});
Console.Clear();

if (result)
    Console.WriteLine("Lyckades");
else
    Console.WriteLine("något gick fel");

Console.ReadKey();