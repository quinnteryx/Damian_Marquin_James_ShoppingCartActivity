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
        Console.WriteLine("----- ORDER SUMMARY -----");
        double total = 0.0;
        foreach (Product item in cart)
        {
            double subtotal = item.Price * item.Stock;
            total += subtotal;
            Console.WriteLine($"Item: {item.Name}({item.Price}) | Grand Total: P{subtotal:F2}");
        }


        double discount = 0.0;

        if (total >= 5000)
            discount = total * 0.1;
        else if (total >= 3000)
            discount = total * 0.05;

        double finalTotal = total - discount;

        Console.WriteLine($"Discount: P{discount:F2}");
        Console.WriteLine($"Final Total: P{finalTotal:F2}");
        Console.WriteLine($"Subtotal: P{total:F2}");
        while (true)
        {
            try
            {
                Console.Write("\nENTER PAYMENT AMOUNT: ");
                double pay = double.Parse(Console.ReadLine());

                if (pay < finalTotal)
                {
                    Console.WriteLine("AMOUNT INVALID");
                }
                else
                {
                    double change = pay - finalTotal;
                    Console.WriteLine($"Paid P{pay:F2} for the cost of P{finalTotal:F2}. Change: P{change:F2}");
                    break;
                }
            }
            catch
            {
                Console.WriteLine("INVALID. Input Numerical Values");
            }
        }

        Console.WriteLine("THANK YOU FOR SHOPPING");
        return total;
    }
    static void Main()
    {
        List<Product> inventory = new List<Product>()
        {
            new Product(121, "soda", 35.0, 150),
            new Product(232, "chips", 25.0, 130),
            new Product(343, "bread", 15.0, 125),
            new Product(454, "water", 10.5, 160),
        };

        

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

            Console.Write("\nInput order.\nType \"done\" if finished.\nType \"s\" to show cart.\nType \"c\" to clear cart.: ");
            string input = Console.ReadLine().ToLower();


            if (input == "s")
            {
                DisplayProduct(cart);

                Pause();
                continue;
            }

            if (input == "done")
            {
                Console.Clear();
                Console.WriteLine("ORDER COMPLETED");
                GetItemTotal(cart);

                Pause();
                break;
            }

            if (input == "c")
            {
                foreach (Product cartItem in cart)
                {
                    // find matching inventory product
                    Product inventoryItem = inventory.Find(p => p.Id == cartItem.Id);

                    if (inventoryItem != null)
                    {
                        inventoryItem.Stock += cartItem.Stock; // restore stock
                    }
                }
                cart.Clear();
                Console.WriteLine("CART CLEARED");
                Pause();
                continue;
            }


            if (!inventory.Exists(p => p.Name.ToLower() == input))//identify if the input is in the menu
            {
                Console.WriteLine("INVALID. Item not in MENU");
                Pause();
                continue;
            }

            try
            {

                Console.Write("INPUT QUANTITY: ");
                int quant = int.Parse(Console.ReadLine());

                if (quant <= 0)
                {
                    Console.WriteLine("INVALID. Input must be greater than 0.");
                    Pause();
                    continue;
                }

                // identify za product from inventory
                Product selectedProduct = inventory.Find(p => p.Name.ToLower() == input);

                if (selectedProduct.Stock <= 0)
                {
                    Console.WriteLine("OUT OF STOCK. Item cannot be added.");
                    Pause();
                    continue;
                }

                // check if already in cart
                Product cartItem = cart.Find(p => p.Name.ToLower() == input);

                if (cartItem != null)
                {
                    cartItem.Stock += quant; // update quantity
                }
                else
                {
                    // will add new product to cart (copy details)
                    cart.Add(new Product(selectedProduct.Id, selectedProduct.Name, selectedProduct.Price, quant));
                }

                Console.WriteLine($"Added {quant} {input}(s) in cart.");

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
            }
        }
                
    }
}