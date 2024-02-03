using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp.MenuService;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<LocalDatabaseContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\SQL-EFC\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30", x => x.MigrationsAssembly(nameof(Infrastructure))));
    services.AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\SQL-EFC\\Infrastructure\\Data\\local_ProductCatalog_dbf.mdf;Integrated Security=True"));

    services.AddScoped<OrderRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<CustomerInfoRepository>();
    services.AddScoped<CustomerAddressRepository>();
    services.AddScoped<AddressRepository>();
    services.AddScoped<OrderService>();

    services.AddSingleton<MenuService>();

}).Build();

var menuService = builder.Services.GetRequiredService<MenuService>();
await menuService.ShowMainMenu();