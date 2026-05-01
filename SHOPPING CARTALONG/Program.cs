using System;

namespace SHOPPING_CARTLONG;

class Program
{
    public static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    static Product[] inventory = new Product[]
    {
        new Product(121, "soda", 35.0, 150, "food"),
        new Product(992, "Porsche 992 GT3 RS", 15000000.0, 5, "car"),
        new Product(232, "chips", 25.0, 130, "food"),
        new Product(565, "juice", 20.0, 140, "drink"),
        new Product(343, "bread", 15.0, 125, "food"),
        new Product(454, "water", 10.5, 160, "drink")
    };

    static void DisplayProduct(Product[] cart)
    {
        Console.WriteLine("----- YOUR CART ------");

        foreach (Product item in cart)
        {
            Console.WriteLine($"-- ID: {item.Id} | Product: {item.Name.ToUpper()} | Price: P{item.Price} | Quantity: {item.Stock} | Category: {item.Category.ToUpper()} --");
        }
    }

    static void DisplayLowStock()
    {
        Console.WriteLine("\n----- LOW STOCK ALERT -----");

        bool hasLowStock = false;

        foreach (var item in inventory)
        {
            if (item.Stock <= 5) 
            {
                Console.WriteLine($"{item.Name} has only {item.Stock} stock(s) left.");
                hasLowStock = true;
            }
        }

        if (!hasLowStock)
        {
            Console.WriteLine("All items are sufficiently stocked.");
        }
    }

    static double GetItemTotal(Product[] cart)
    {
        Pause();
        Console.WriteLine("----- ORDER SUMMARY -----\n");

        double total = 0.0;

        for (int i = 0; i < cart.Length; i++) // 
        {
            Product item = cart[i];
            double subtotal = item.Price * item.Stock;
            total += subtotal;

            Console.WriteLine($"-- {item.Name.ToUpper()} | Qty:{item.Stock} | Subtotal: P{subtotal:F2} --");
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
                Console.Write("Proceed with Payment? (Y/N): ");
                string proceed = Console.ReadLine().ToUpper();
                if (proceed == "Y")
                {
                    double change = pay - finalTotal;
                    Console.WriteLine("PAYMENT SUCCESSFUL");

                    Console.WriteLine("\n----- RECEIPT -----");
                    string receiptNo = receiptCounter.ToString("D4");
                    string record = $"Receipt #{receiptNo} - Final Total: PHP {finalTotal:F2} | Date: {DateTime.Now.ToString()}";
                    string[] newHistory = new string[orderHistory.Length + 1];
                    string dateNow = DateTime.Now.ToString("MMMM dd, yyyy h:mm tt");


                    Console.WriteLine($"Receipt No: {receiptNo}");
                    Console.WriteLine($"Date: {dateNow}\n");

                    Console.WriteLine("\n---- PURCHASED ITEMS ----");
                    DisplayProduct(cart);
                    Console.WriteLine($"\nDiscount: P{discount:F2}");
                    Console.WriteLine($"Subtotal: P{total:F2}");
                    Console.WriteLine($"Final Total: P{finalTotal:F2}");
                    Console.WriteLine($"Paid: P{pay:F2}");
                    Console.WriteLine($"Change: P{change:F2}");
                    

                    for (int i = 0; i < orderHistory.Length; i++)
                    {
                        newHistory[i] = orderHistory[i];
                    }
                    newHistory[orderHistory.Length] = record;

                    orderHistory = newHistory;

                    receiptCounter++;

                    DisplayLowStock();
                }
                else if (proceed == "N")
                {
                    Console.WriteLine("Payment cancelled. Returning to menu.");
                    Pause();
                    return total;
                }
                else
                {
                    Console.WriteLine("INVALID INPUT");
                }

                while (true)
                {
                    Console.Write("\nShop again? (Y/N): ");
                    string again = Console.ReadLine().ToUpper();

                    if (again == "Y")
                    {
                        return total;
                    }

                    if (again == "N")
                    {
                        Pause();
                        Environment.Exit(0);
                    }
                    Console.WriteLine("INVALID INPUT");
                }
            }
            catch
            {
                Console.WriteLine("INVALID INPUT");
            }
        } 
    }
    public static void DisplayOrderHistory()
    {
            Console.WriteLine("\n----- ORDER HISTORY -----");

            if (orderHistory.Length == 0)
            {
                Console.WriteLine("No transactions yet.");
                return;
            }

            foreach (var record in orderHistory)
            {
                Console.WriteLine(record);
            }
    }
   
    static string[] orderHistory = new string[0];
    static int receiptCounter = 1;

    static void Main()
    {
        Product[] cart = new Product[0];

        while (true)
        {
            Console.WriteLine("WELCOME TO FOOD APP");
            Pause();
            Console.WriteLine("WELCOME TO FOOD APP");

            Console.WriteLine("\n----- MENU -----");

            foreach (var p in inventory)
            {
                Console.WriteLine($"{p.Id} - {p.Name} - P{p.Price} - Stock:{p.Stock} - Category:{p.Category}");
            }

            Console.WriteLine("\nOPTIONS:");
            Console.WriteLine("1 - Show Cart");
            Console.WriteLine("2 - Checkout");
            Console.WriteLine("3 - Clear Cart");
            Console.WriteLine("4 - Remove an item from Cart");
            Console.WriteLine("5 - Search Product/Category");
            Console.WriteLine("6 - Shopping History");
            Console.WriteLine("7 - Exit");

            Console.Write("\nEnter choice or product ID or product Name: ");

            try
            {
                string input = Console.ReadLine().ToLower();

                // SHOW CART
                if (input == "1")
                {
                    if (cart.Length > 0)
                        DisplayProduct(cart); //
                    else
                        Console.WriteLine("CART IS EMPTY");

                    Pause();
                    continue;
                }

                // CHECKOUT
                if (input == "2")
                {
                    if (cart.Length == 0)
                    {
                        Console.WriteLine("Cart is empty!");
                        Pause();
                        continue;
                    }
                    Console.Write("Are you sure you want to proceed to checkout? (Y/N): ");
                    string validation = Console.ReadLine().ToUpper();

                    if (validation == "Y")
                    {
                        GetItemTotal(cart);
                        cart = new Product[0]; // reset cart
                        continue;
                    }
                    else if (validation == "N")
                    {
                        Console.WriteLine("Checkout cancelled.");
                        Pause();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("INVALID INPUT");
                        Pause();
                        continue;
                    }

                }

                // CLEAR CART
                if (input == "3")
                {
                    for (int i = 0; i < cart.Length; i++)
                    {
                        for (int j = 0; j < inventory.Length; j++)
                        {
                            if (cart[i].Id == inventory[j].Id)
                                inventory[j].Stock += cart[i].Stock;
                        }
                    }

                    cart = new Product[0];
                    Console.WriteLine("CART CLEARED");
                    Pause();
                    continue;
                }

                if (input == "4")
                {
                    Console.Write("Enter product ID or Name to remove: ");
                    string removeInput = Console.ReadLine().ToLower();

                    foreach (var i in cart)
                    {
                        if (i.Name == removeInput || i.Id.ToString() == removeInput)
                        {
                            Console.Write("Enter quantity to remove: ");
                            int quantityToRemove = int.Parse(Console.ReadLine()); // Store the quantity to remove before modifying the cart

                            for (int j = 0; j < inventory.Length; j++)
                            {
                                if (i.Id == inventory[j].Id)
                                    inventory[j].Stock += quantityToRemove;
                            }
                            cart = cart.Where(p => p.Id != i.Id).ToArray();
                            Console.WriteLine($"Removed {quantityToRemove} {i.Name}(s) from cart.");
                            Pause();
                            break;
                        }
                    }

                    if (removeInput != cart.Length.ToString())
                    {
                        Console.WriteLine("Invalid input.");
                        Pause();
                        continue;
                    }

                }

                if (input == "5")
                {
                    Console.Clear();
                    Console.Write("Search Product/Category: ");
                    string search = Console.ReadLine().ToLower();

                    Console.WriteLine($"\n{"ID"} --- {"Product"} --- {"Price"} --- {"Stock"} --- {"Category"}");

                    bool isFound = false;

                    foreach (Product p in inventory)
                    {
                        if (p.Name.ToLower().Contains(search) || p.Category.ToLower().Contains(search))
                        {
                            Console.WriteLine($"{p.Id} --- {p.Name} --- P{p.Price} --- {p.Stock} --- {p.Category}\n");
                            isFound = true;
                        }
                    }

                    if (!isFound)
                    {
                        Console.WriteLine("No matching products found.");
                    }

                    Pause();
                    continue;
                }

                if (input == "6")
                {
                    DisplayOrderHistory();
                    Pause();
                    continue;
                }

                if (input == "7")
                {
                    Console.WriteLine("THANK YOU FOR SHOPPING:) (closing...)");
                    Pause();
                    Environment.Exit(0);
                }

                // FIND PRODUCT
                Product selected = null;

                foreach (var p in inventory)
                {
                    if (p.Name == input || p.Id.ToString() == input)
                    {
                        selected = p;
                        break;
                    }
                }

                if (selected == null)
                {
                    Console.WriteLine("Invalid product.");
                    Pause();
                    continue;
                }

                Console.Write("INPUT QUANTITY: ");
                int quant = int.Parse(Console.ReadLine());

                if (quant <= 0 || quant > selected.Stock)
                {
                    Console.WriteLine("Not enough stock.");
                    Pause();
                    continue;
                }

                // CHECK IF EXISTS
                bool found = false;

                for (int i = 0; i < cart.Length; i++)
                {
                    if (cart[i].Id == selected.Id)
                    {
                        cart[i].Stock += quant;
                        found = true;
                        break;
                    }
                }

                // ADD NEW ITEM
                if (!found)
                {
                    Product[] newCart = new Product[cart.Length + 1];

                    for (int i = 0; i < cart.Length; i++)
                        newCart[i] = cart[i];

                    newCart[cart.Length] = new Product(selected.Id, selected.Name, selected.Price, quant, selected.Category);

                    cart = newCart;
                }

                selected.Stock -= quant; // 

                Console.WriteLine($"Added {quant} {selected.Name}(s) to cart.");
                DisplayLowStock();  

                Console.Write("Continue shopping? (Y/N): ");
                string decide = Console.ReadLine().ToUpper();

                if (decide == "Y")
                {
                    Pause();
                    continue;
                }
                else
                {
                    Console.Write("Proceed to checkout? (Y/N || C to cancel): ");
                    string checkout = Console.ReadLine().ToUpper();
                    if (checkout == "Y")
                    {
                        GetItemTotal(cart);
                        cart = new Product[0]; // reset cart
                        continue;
                    }
                    else if (checkout == "N")
                    {
                        Console.WriteLine("Checkout cancelled. Terminating Program.");
                        Pause();
                        Environment.Exit(0);
                    }
                    else if (checkout == "C")
                    {
                        Console.WriteLine("Checkout cancelled. Returning to menu.");
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
            }
            catch
            {
                Console.WriteLine("INVALID INPUT");
                Pause();
            }
        }
    }
}