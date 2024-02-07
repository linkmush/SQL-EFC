using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Presentation.ConsoleApp.MenuService;

public class MenuService(IOrderService orderService, IProductService productService, ICurrencyRepository currencyRepository)
{
    private readonly IOrderService _orderService = orderService;
    private readonly IProductService _productService = productService;
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;

    public async Task ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----------MENU---------");
            Console.WriteLine();
            Console.WriteLine("Choose between project");
            Console.WriteLine();
            Console.WriteLine("1. Code First:");
            Console.WriteLine("2. Database First");
            Console.WriteLine("3. EXIT");


            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ShowCodeFirstMenu();
                    break;
                case "2":
                    await ShowDataBaseFirstMenu();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }

            Console.ReadKey();
        }
    }

    public async Task ShowCodeFirstMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----MENU CODE FIRST----");
            Console.WriteLine();
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Show all customers");
            Console.WriteLine("3. Show specific customer");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Delete customer");
            Console.WriteLine("6. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowAddMenuAsync();
                    break;
                case "2":
                    await ShowAllMenuAsync();
                    break;
                case "3":
                    await ShowObjectMenuAsync();
                    break;
                case "4":
                    await ShowUpdateMenuAsync();
                    break;
                case "5":
                    await ShowDeletedMenuAsync();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }

            Console.ReadKey();
        }
    }

    public async Task ShowDataBaseFirstMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----MENU DATABASE FIRST----");
            Console.WriteLine();
            Console.WriteLine("1. Add new Product");
            Console.WriteLine("2. Show all Products");
            Console.WriteLine("3. Show specific product");
            Console.WriteLine("4. Update product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("6. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowAddProductAsync();
                    break;
                case "2":
                    await ShowAllProductsAsync();
                    break;
                case "3":
                    await ShowObjectProductAsync();
                    break;
                case "4":
                    await ShowUpdateProductAsync();
                    break;
                case "5":
                    await ShowDeletedProductAsync();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }

            Console.ReadKey();
        }
    }

    public async Task ShowAddProductAsync()
    {
        ProductDto product = new();

        Console.Clear();
        Console.WriteLine("Enter Title:  ");
        product.Title = Console.ReadLine()!;

        Console.WriteLine("Enter preamble:  ");
        product.Preamble = Console.ReadLine()!;

        Console.WriteLine("Enter description:  ");
        product.Description = Console.ReadLine()!;

        Console.WriteLine("Enter specification:  ");
        product.Specification = Console.ReadLine()!;

        Console.WriteLine("Enter categoryname:  ");
        product.Category.CategoryName = Console.ReadLine()!;

        Console.WriteLine("Enter manufacturer:  ");
        product.Manufacturer.Manufacture = Console.ReadLine()!;

        Console.WriteLine("Enter price:  ");
        product.ProductPrice.Price = decimal.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter currency code:  ");
        string currencyCode = Console.ReadLine()!;

        Currency existingCurrency = await _currencyRepository.GetOneAsync(x => x.Code == currencyCode);

        if (existingCurrency == null)
        {
            Console.WriteLine("Currency code not found. Enter full currency name: ");
            string currencyName = Console.ReadLine()!;

            Currency newCurrency = new Currency
            {
                Code = currencyCode,
                Currency1 = currencyName
            };

            await _currencyRepository.CreateAsync(newCurrency);

            product.ProductPrice.Currency = new CurrencyDto
            {
                Code = currencyCode,
                Currency1 = currencyName
            };
        }
        else
        {
            product.ProductPrice.Currency = new CurrencyDto
            {
                Code = existingCurrency.Code,
                Currency1 = existingCurrency.Currency1
            };
        }

        await _productService.CreateProductAsync(product);

        Console.WriteLine();
        Console.WriteLine("Product successfully added!");

        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }

    public async Task ShowAllProductsAsync()
    {
        var result = await _productService.GetAllAsync();

        if (result != null)
        {
            var users = result as List<ProductDto>;

            if (users != null && users.Any())
            {
                Console.Clear();
                int count = 1;
                foreach (var user in users)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{count}. ");
                    Console.WriteLine($"Article Number: {user.ArticleNumber}");
                    Console.WriteLine($"{user.Title}");
                    Console.WriteLine($"{user.Preamble}");
                    Console.WriteLine($"{user.Description} ");
                    Console.WriteLine($"{user.Specification}");
                    Console.WriteLine($"{user.Manufacturer.Manufacture} ");
                    Console.WriteLine($"{user.Category.CategoryName}");
                    Console.WriteLine($"{user.ProductPrice.Price} {user.ProductPrice.Currency.Code} {user.ProductPrice.Currency.Currency1}  ");
                    Console.WriteLine();

                    count++;
                }
                Console.WriteLine("-----PRESS ANY KEY TO RETURN TO MENU-----");
            }
            else
            {
                Console.WriteLine("No contacts found.");
            }
        }
        else
        {
            Console.WriteLine("Failed to retrieve contacts.");
        }
    }

    public async Task ShowObjectProductAsync()
    {
        Console.WriteLine("Type the article number of the product you want to retrieve: ");

        var articleNumberInput = Console.ReadLine();

        if (int.TryParse(articleNumberInput, out int articleNumber))
        {
            var result = await _productService.GetOneAsync(x => x.ArticleNumber  == articleNumber);

            Console.Clear();

            if (result != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Article Number: {result.ArticleNumber}");
                Console.WriteLine($"{result.Title}");
                Console.WriteLine($"{result.Preamble}");
                Console.WriteLine($"{result.Description} ");
                Console.WriteLine($"{result.Specification}");
                Console.WriteLine($"{result.Manufacturer.Manufacture} ");
                Console.WriteLine($"{result.Category.CategoryName}");
                Console.WriteLine($"{result.ProductPrice.Price} {result.ProductPrice.Currency.Code} {result.ProductPrice.Currency.Currency1}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("-----PRESS ANY KEY TO RETURN TO MENU-----");
            }
            else
            {
                Console.WriteLine("No contact found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid email address.");
        }
    }

    public async Task ShowUpdateProductAsync()
    {
        Console.WriteLine("Type the article number of the product you want to update: ");

        var articleNumberInput = Console.ReadLine();

        if (int.TryParse(articleNumberInput, out int articleNumber))
        {
            var producttoUpdate = await _productService.GetOneAsync(x => x.ArticleNumber == articleNumber);

            if (producttoUpdate != null)
            {
                Console.Clear();
                Console.WriteLine("Enter new product details:");
                Console.WriteLine("Enter Title:  ");
                producttoUpdate.Title = Console.ReadLine()!;

                Console.WriteLine("Enter new preamble:  ");
                producttoUpdate.Preamble = Console.ReadLine()!;

                Console.WriteLine("Enter new description:  ");
                producttoUpdate.Description = Console.ReadLine()!;

                Console.WriteLine("Enter new specification:  ");
                producttoUpdate.Specification = Console.ReadLine()!;

                Console.WriteLine("Enter new categoryname:  ");
                producttoUpdate.Category.CategoryName = Console.ReadLine()!;

                Console.WriteLine("Enter new manufacturer:  ");
                producttoUpdate.Manufacturer.Manufacture = Console.ReadLine()!;

                Console.WriteLine("Enter new price:  ");
                producttoUpdate.ProductPrice.Price = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter new currency code:  ");
                producttoUpdate.ProductPrice.Currency.Code = Console.ReadLine()!;

                Console.WriteLine("Enter new currency full name:  ");
                producttoUpdate.ProductPrice.Currency.Currency1 = Console.ReadLine()!;

                var updatedCustomerResult = await _productService.UpdateAsync(producttoUpdate);

                if (updatedCustomerResult != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Product successfully updated!");

                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Failed to update product");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
            }
        }
    }

    public async Task ShowDeletedProductAsync()
    {
        Console.WriteLine("Type the article number of the product you want to delete: ");

        var articleNumberInput = Console.ReadLine();

        if (int.TryParse(articleNumberInput, out int articleNumber))
        {
            var deleteResult = await _productService.DeleteAsync(new ProductDto { ArticleNumber = articleNumber });

            if (deleteResult)
            {
                Console.WriteLine();
                Console.WriteLine("Product successfully deleted!");
            }
            else
            {
                Console.WriteLine("Failed to delete the product.");
            }
        }
        else
        {
            Console.WriteLine("Invalid article number.");
        }
    }

    public async Task ShowAddMenuAsync()
    {
        CustomerRegistrationDto customer = new();

        Console.Clear();
        Console.WriteLine("Enter First Name:  ");
        customer.FirstName = Console.ReadLine()!;

        Console.WriteLine("Enter Last Name:  ");
        customer.LastName = Console.ReadLine()!;

        Console.WriteLine("Enter Email:  ");
        customer.Email = Console.ReadLine()!;

        Console.WriteLine("Enter street name:  ");
        customer.StreetName = Console.ReadLine()!;

        Console.WriteLine("Enter postal code:  ");
        customer.PostalCode = Console.ReadLine()!;

        Console.WriteLine("Enter city:  ");
        customer.City = Console.ReadLine()!;

        await _orderService.CreateCustomerAsync(customer);

        Console.WriteLine();
        Console.WriteLine("Customer successfully added!");

        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();

    }

    public async Task ShowAllMenuAsync()
    {
        var result = await _orderService.GetAllAsync();

        if (result != null)
        {
            var users = result as List<CustomerDto>;

            if (users != null && users.Any())
            {
                Console.Clear();
                int count = 1;
                foreach (var user in users)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{count}. ");
                    Console.WriteLine($"{user.FirstName} {user.LastName} ");
                    Console.WriteLine($"{user.Email} ");

                    foreach (var address in user.Addresses)
                    {
                        Console.WriteLine($"{address.StreetName} ");
                        Console.WriteLine($"{address.PostalCode} ");
                        Console.WriteLine($"{address.City} ");
                    }
                    Console.WriteLine();

                    count++;
                }
                Console.WriteLine("-----PRESS ANY KEY TO RETURN TO MENU-----");
            }
            else
            {
                Console.WriteLine("No contacts found.");
            }
        }
        else
        {
            Console.WriteLine("Failed to retrieve contacts.");
        }
    }

    public async Task ShowObjectMenuAsync()
    {
        Console.WriteLine("Type the email address of the contact you want to retrieve: ");

        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var result = await _orderService.GetOneAsync(x => x.Email == email);

            Console.Clear();

            if (result != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{result.FirstName} {result.LastName} ");
                Console.WriteLine($"{result.Email} ");

                foreach (var address in result.Addresses)
                {
                    Console.WriteLine($"{address.StreetName} ");
                    Console.WriteLine($"{address.PostalCode} ");
                    Console.WriteLine($"{address.City} ");
                }
                Console.WriteLine();
                Console.WriteLine("-----PRESS ANY KEY TO RETURN TO MENU-----");
            }
            else
            {
                Console.WriteLine("No contact found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid email address.");
        }
    }

    public async Task ShowUpdateMenuAsync()
    {
        Console.WriteLine("Type the email address of the contact you want to update: ");

        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var customertoUpdate = await _orderService.GetOneAsync(x => x.Email == email);

            if (customertoUpdate != null)
            {
                Console.Clear();
                Console.WriteLine("Enter new customer details:");
                Console.WriteLine("Enter First Name:  ");
                customertoUpdate.FirstName = Console.ReadLine()!;

                Console.WriteLine("Enter Last Name:  ");
                customertoUpdate.LastName = Console.ReadLine()!;

                Console.WriteLine("Enter Email:  ");
                customertoUpdate.Email = Console.ReadLine()!;

                foreach (var address in customertoUpdate.Addresses)
                {
                    Console.WriteLine("Enter StreetName:   ");
                    address.StreetName = Console.ReadLine()!;

                    Console.WriteLine("Enter PostalCode:   ");
                    address.PostalCode = Console.ReadLine()!;

                    Console.WriteLine("Enter City:   ");
                    address.City = Console.ReadLine()!;
                }

                var updatedCustomerResult = await _orderService.UpdateAsync(customertoUpdate);

                if (updatedCustomerResult != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Customer successfully updated!");

                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Failed to update customer");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }
            }
        }
    }

    public async Task ShowDeletedMenuAsync()
    {
        Console.WriteLine("Type the email address of the contact you want to delete: ");

        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var customerToDelete = await _orderService.DeleteAsync(new CustomerDto { Email = email });

            if (customerToDelete)
            {
                Console.WriteLine();
                Console.WriteLine("Customer successfully deleted!");
            }
            else
            {
                Console.WriteLine("Failed to delete the customer.");
            }
        }
        else
        {
            Console.WriteLine("Invalid email address.");
        }
    }
}
