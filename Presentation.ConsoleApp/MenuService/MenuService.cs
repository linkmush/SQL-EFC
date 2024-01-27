using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;
using System.Net.NetworkInformation;

namespace Presentation.ConsoleApp.MenuService;

public class MenuService(OrderService orderService)
{
    private readonly OrderService _orderService = orderService;


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
               // case "2":
                 //   await ShowDataBaseFirstMenu();
                  //  break;
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

    public async Task ShowCodeFirstMenu ()
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

    public async Task ShowAddMenuAsync()
    {
        CustomerDto customer = new();

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

        if (result !=null)
        {
            var users = result as List<CustomerDto>;

            if (users != null && users.Any())
            {
                int count = 1;
                foreach (var user in users)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{count}. ");
                    Console.WriteLine($"{user.FirstName} {user.LastName} ");
                    Console.WriteLine($"{user.Email} ");
                    Console.WriteLine($"{user.StreetName} ");
                    Console.WriteLine($"{user.PostalCode} ");
                    Console.WriteLine($"{user.City} ");
                    Console.WriteLine();

                    count++;
                }
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

            if (result != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{result.FirstName} {result.LastName} ");
                Console.WriteLine($"{result.Email} ");
                Console.WriteLine($"{result.StreetName} ");
                Console.WriteLine($"{result.PostalCode} ");
                Console.WriteLine($"{result.City} ");
                Console.WriteLine();
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

                Console.WriteLine("Enter street name:  ");
                customertoUpdate.StreetName = Console.ReadLine()!;

                Console.WriteLine("Enter postal code:  ");
                customertoUpdate.PostalCode = Console.ReadLine()!;

                Console.WriteLine("Enter city:  ");
                customertoUpdate.City = Console.ReadLine()!;

                await _orderService.UpdateAsync(customertoUpdate);

                Console.WriteLine();
                Console.WriteLine("Customer successfully updated!");

                Console.WriteLine();
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
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
