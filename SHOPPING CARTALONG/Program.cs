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
    static void Main()
    {
        List<Product> inventory = new List<Product>()
        {
            new Product(1, "soda", 20.0, 50),
            new Product(2, "chips", 15.0, 30),
            new Product(3, "bread", 10.0, 25),
            new Product(4, "water", 5.5, 60),
        };

        

        List<Product> cart = new List<Product>();
        double total = 0.0;

        Console.WriteLine("WELCOME TO FOOD APP");
        Console.WriteLine("Please take your order:");
        Console.WriteLine("-----MENU-----");

        foreach (Product product in inventory)
        {
            Console.WriteLine($"-- ID: {product.Id} | Product: {product.Name.ToUpper()}: P{product.Price}| Quantity:{product.Stock} -- ");
        }
        Console.WriteLine("-----------");

        while (true)
        {
            Console.Write("\nInput order.\nType \"done\" if finished.\nType \"s\" to show cart.\nType \"c\" to clear cart.: ");
            string input = Console.ReadLine().ToLower();

            if (input == "done")
            {
                Console.WriteLine("ORDER COMPLETED");
                break;
            }

            if (input == "c")
            {
                cart.Clear();
                Console.WriteLine("CART CLEARED");
                continue;
            }
            if(input == "s")
            { 
                foreach (Product item in cart)
                {
                    Console.WriteLine("----- YOUR CART ------");
                    Console.WriteLine($"-- ID: {item.Id} | Product: {item.Name.ToUpper()}: P{item.Price}| Quantity:{item.Stock} -- ");

                }
            }

            //if (!inventory.ContainsKey(input))
            //{
            //    Console.WriteLine("INVALID. Item not in MENU");
            //    continue;
            //}

            try
            {
                Console.Write("INPUT QUANTITY: ");
                int quant = int.Parse(Console.ReadLine());

                if (quant <= 0)
                {
                    Console.WriteLine("INVALID. Input must be greater than 0.");
                    continue;
                }

                //if (cart.ContainsKey(input))
                //    cart[input] += quant;
                //else
                //    cart[input] = quant;

                Console.WriteLine($"Added {quant} {input}(s) in cart.");
            }
            catch
            {
                Console.WriteLine("INVALID INPUT. Please enter correctly.");
            }
        }

        Console.WriteLine("-----ORDER SUMMARY-----");

        foreach (Product item in cart)
        {
            double subtotal = item.Price * item.Stock;
            total += subtotal;
            Console.WriteLine($"Item: {item.Name}({item.Price}) = P{subtotal:F2}");
        }

        Console.WriteLine($"Subtotal: P{total:F2}");

        double discount = 0.0;

        if (total >= 500)
            discount = total * 0.2;
        else if (total >= 300)
            discount = total * 0.1;

        double finalTotal = total - discount;

        Console.WriteLine($"Discount: P{discount:F2}");
        Console.WriteLine($"Total after discount: P{finalTotal:F2}");

        while (true)
        {
            try
            {
                Console.Write("ENTER PAYMENT AMOUNT: ");
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
    }
}