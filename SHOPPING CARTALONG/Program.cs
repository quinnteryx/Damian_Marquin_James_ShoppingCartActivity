using System;
using System.Collections.Generic;

namespace SHOPPING_CARTLONG;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public  int Stock { get; set; }
    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }
}
class Program
{
    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    static void DisplayProduct(List<Product> cart)
    {
        Console.WriteLine("----- YOUR CART ------");
        foreach (Product item in cart)
        {
            Console.WriteLine($"-- ID: {item.Id} | Product: {item.Name.ToUpper()}: P{item.Price}| Quantity:{item.Stock} -- ");
        }
    }
    static double GetItemTotal(List<Product> cart)
    {
        Console.WriteLine("----- ORDER SUMMARY -----\n");
        double total = 0.0;
        foreach (Product item in cart)
        {
            double subtotal = item.Price * item.Stock;
            total += subtotal;
            Console.WriteLine($"Item: {item.Name} - Price:{item.Price} - Quantity:{item.Stock}| Grand Total: P{subtotal:F2}");
        }


        double discount = 0.0;

        if (total >= 5000)
            discount = total * 0.1;
        else if (total >= 3000)
            discount = total * 0.05;

        double finalTotal = total - discount;

        Console.WriteLine($"Discount: P{discount:F2}");
        Console.WriteLine($"Subtotal: P{total:F2}");
        Console.WriteLine($"Final Total: P{finalTotal:F2}");
        while (true)
        {
            try
            {
                Console.Write("\nENTER PAYMENT AMOUNT: ");
                double pay = double.Parse(Console.ReadLine());

                if (pay < finalTotal)
                {
                    Console.WriteLine("AMOUNT INVALID");
                    continue;
                }
                else
                {
                    double change = pay - finalTotal;
                    Console.WriteLine($"Paid P{pay:F2} for the cost of P{finalTotal:F2}. Change: P{change:F2}");

                    Console.WriteLine("\n----- RECEIPT -----");
                    foreach (Product cartItem in cart)
                    {
                        Console.WriteLine($"-- ID: {cartItem.Id} | Product: {cartItem.Name.ToUpper()}: P{cartItem.Price}| Quantity:{cartItem.Stock} -- ");
                    }

                    while (true)
                    {

                        Console.WriteLine("\nTHANK YOU FOR SHOPPING\n");
                        Console.Write("Do you wish to shop again? (yes/no): ");
                        string shopAgain = Console.ReadLine();
                        if (shopAgain.ToLower() == "yes")
                        {
                            Console.WriteLine("\nREDIRECTING TO MENU...");
                            Pause();
                            return total;
                        }
                        else if (shopAgain.ToLower() == "no")
                        {
                            Console.WriteLine("\nTHANK YOU FOR SHOPPING\n");
                            Console.Clear();
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("INVALID INPUT. .");
                            continue;
                        }
                    }
                }


            }
            catch
            {
                Console.WriteLine("INVALID. Input Numerical Values");
            }

        }


    }
    static List<Product> inventory = new List<Product>()
    {
        new Product(121, "soda", 35.0, 150),
        new Product(232, "chips", 25.0, 130),
        new Product(343, "bread", 15.0, 125),
        new Product(454, "water", 10.5, 160),
    }; 
    static void Main()
    {        

        List<Product> cart = new List<Product>();//creates new list for cart... empty at the start
        double total = 0.0;

        while (true)
        {
            Console.Clear(); //Clears the previous text outputs
            Console.WriteLine("WELCOME TO FOOD APP");
            Console.WriteLine("Please take your order:");

            Console.WriteLine("-----MENU-----");

            foreach (Product product in inventory)
            {
                Console.WriteLine($"-- ID: {product.Id} | Product: {product.Name.ToUpper()}: P{product.Price}| Quantity:{product.Stock} -- ");
            }
            Console.WriteLine("-----------");

            Console.WriteLine("OPTIONS:");
            Console.WriteLine("1 - Show Cart");
            Console.WriteLine("2 - Complete Order");
            Console.WriteLine("3 - Clear Cart");
            Console.Write("Enter the ID of the product you want to add to cart or choose an option: ");

            
            try
            {
                int input = int.Parse(Console.ReadLine());


                if (input == 1)
                {
                    DisplayProduct(cart);

                    Pause();
                    continue;
                }

                if (input == 2)
                {
                    Console.Write("Are you sure you want to proceed to checkout? (yes/no): ");
                    string validation = Console.ReadLine();
                    if (validation.ToLower() == "yes")
                    {
                        Console.WriteLine("ORDER COMPLETED");
                        Pause();
                        Console.Clear();
                        GetItemTotal(cart);
                        continue;
                    }
                    else if (validation.ToLower() == "no")
                    {
                        Console.WriteLine("ORDER NOT COMPLETED");
                        Pause();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("INVALID INPUT. Returning to menu.");
                        Pause();
                        continue;
                    }
                }

                if (input == 3)
                {
                    foreach (Product cartItem2 in cart)
                    {
                        // find matching inventory product
                        Product inventoryItem = inventory.Find(p => p.Id == cartItem2.Id);

                        if (inventoryItem != null)
                        {
                            inventoryItem.Stock += cartItem2.Stock; // restore stock
                        }
                    }
                    cart.Clear();
                    Console.WriteLine("CART CLEARED");
                    Pause();
                    continue;
                }


                if (!inventory.Exists(p => p.Id == input))//identify if the input is in the menu
                {
                    Console.WriteLine("INVALID. Item not in MENU");
                    Pause();
                    continue;
                }


                Console.Write("INPUT QUANTITY: ");
                int quant = int.Parse(Console.ReadLine());

                if (quant <= 0)
                {
                    Console.WriteLine("INVALID. Input must be greater than 0.");
                    Pause();
                    continue;
                }

                // identify za product from inventory
                Product selectedProduct = inventory.Find(p => p.Id == input);

                if (selectedProduct.Stock <= 0)
                {
                    Console.WriteLine("OUT OF STOCK. Item cannot be added.");
                    Pause();
                    continue;
                }

                if (quant > selectedProduct.Stock)
                {
                    Console.WriteLine("Not enough stock available.");
                    Pause();
                    continue;
                }

                // check if already in cart
                Product cartItem = cart.Find(p => p.Id == input);

                if (cartItem != null)
                {
                    cartItem.Stock += quant; // update quantity
                }
                else
                {
                    // will add new product to cart (copy details)
                    cart.Add(new Product(selectedProduct.Id, selectedProduct.Name, selectedProduct.Price, quant));
                }

                Console.WriteLine($"Added {quant} {selectedProduct.Name}(s) in cart.");

                if (quant > selectedProduct.Stock)
                {
                    Console.WriteLine("Not enough stock available.");
                    continue;
                }

                selectedProduct.Stock -= quant; // reduce inventory
                Pause();
            }
            catch
            {
                Console.WriteLine("INVALID INPUT. Please enter correctly.");
                Pause();
            }
        }
                
    }
}