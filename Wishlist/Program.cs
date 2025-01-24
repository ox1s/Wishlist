using System.Data.Common;
using System.Linq.Expressions;
using System.Security;

class Program
{
    static void Main(string[] args)
    {
        string menuSelection = "-1";
        int itemsMax = int.MaxValue;
        string? readResult = Console.ReadLine();
        List<Item> items = new List<Item>();

        do
        {
            // display the top-level menu options
            Console.Clear();

            Console.WriteLine("Welcome to the Wishlist app. Your main menu options are:");
            Console.WriteLine(" 1. Add a new item to our list");
            Console.WriteLine(" 2. Make as purchased");
            Console.WriteLine(" 3. Set price to item");
            Console.WriteLine(" 4. List all of our items");
            Console.WriteLine();
            Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

            if (readResult != null)
            {
                menuSelection = readResult.ToLower();
            }

            do
            {
                switch (menuSelection)
                {
                    case "4":
                        foreach (var item in items)
                        {
                            item.GetItemDetails();
                        }
                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();

                        break;

                    case "1s":
                        string addAnotherItem = "y";

                        while (addAnotherItem == "y" && items.Count < itemsMax)
                        {
                            bool validEntry = false;
                            string? input = null;

                            do
                            {
                                Console.WriteLine("\n\rEnter 'category' or 'cat' to select a item category or enter 'n' if this is not necessary");
                                readResult = Console.ReadLine();
                                input = readResult;
                                if (readResult != null)
                                {
                                    input = readResult.ToLower();
                                    if (input != "category" && input != "cat" && input != "n")
                                    {
                                        Console.WriteLine($"You entered: {input}.");
                                        validEntry = false;
                                    }
                                    else
                                    {
                                        validEntry = true;
                                    }
                                }
                            } while (validEntry == false);


                            do
                            {
                                if (input != "n")
                                {
                                    string? category;
                                    Console.WriteLine("Enter categoty\n- el(for electronic)\n- c(for clothes)\n - b(for books)");
                                    readResult = Console.ReadLine();
                                    if (readResult != null)
                                    {
                                        category = readResult;
                                        if (category != "n")
                                        {
                                            switch (category)
                                            {
                                                case "el":
                                                    Console.WriteLine("Enter the name of the electronic");
                                                    readResult = Console.ReadLine();
                                                    if (readResult != null)
                                                    {
                                                        string? electronicName = readResult;
                                                        string model = "-";
                                                        string brand = "-";
                                                        if (electronicName != null)
                                                        {
                                                            Console.WriteLine("Do you want to enter Model and Brand of the electronic?(y/n)");
                                                            readResult = Console.ReadLine();
                                                            if (readResult != null)
                                                            {
                                                                string? answer = readResult;
                                                                if (answer == "y")
                                                                {
                                                                    Console.WriteLine("Enter Model of the electronic");
                                                                    readResult = Console.ReadLine();
                                                                    if (readResult != null)
                                                                    {
                                                                        model = readResult;
                                                                        Console.WriteLine("Enter Brand of the electronic");
                                                                        readResult = Console.ReadLine();
                                                                        if (readResult != null)
                                                                        {
                                                                            brand = readResult;
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                            Console.WriteLine($"Enter the price of the {electronicName}");
                                                            readResult = Console.ReadLine();
                                                            if (decimal.TryParse(readResult, out decimal electronicPrice))
                                                            {
                                                                ElectronicItem electronicItem = new ElectronicItem(items.Count+1, electronicName, electronicPrice, category, brand, model);
                                                                items.Add(electronicItem);
                                                                Console.WriteLine($"You entered: \ncategory: Electrionic \n\titem: {electronicName}\n\t\tprice: {electronicPrice}$.");
                                                                validEntry = true;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "c":
                                                    Console.WriteLine("Enter the name of the clothes");
                                                    readResult = Console.ReadLine();
                                                    if (readResult != null)
                                                    {
                                                        string? clothingName = readResult;
                                                        if (clothingName != null)
                                                        {
                                                            Console.WriteLine("Enter the price of the clothes");
                                                            readResult = Console.ReadLine();
                                                            if (decimal.TryParse(readResult, out decimal clothingPrice))
                                                            {
                                                                ClothingItem clothingItem = new ClothingItem(items.Count+1, clothingName, clothingPrice, category, "X", "blue");
                                                                items.Add(clothingItem);
                                                                Console.WriteLine($"You entered: \ncategory: Clothing \n\titem: {clothingName}\n\t\tprice: {clothingPrice}$\nSize:\n\t\t{clothingItem.Size}\nColor:\n\t\t{clothingItem.Color}");
                                                                validEntry = true;
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            validEntry = true;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Enter the name of item");
                                    readResult = Console.ReadLine();
                                    if (readResult != null)
                                    {
                                        string? itemName = readResult;
                                        if (itemName != null)
                                        {
                                            Console.WriteLine($"Enter the price of the {itemName}");
                                            readResult = Console.ReadLine();
                                            if (decimal.TryParse(readResult, out decimal itemPrice))
                                            {
                                                Item item = new Item(items.Count + 1, itemName, itemPrice);
                                                items.Add(item);
                                                Console.WriteLine($"You entered: \ncategory: Clothing \n\titem: {itemName}\n\t\tprice: {itemPrice}$.");
                                                validEntry = true;
                                            }
                                        }
                                    }
                                    validEntry = true;
                                }
                            } while (validEntry == false);


                            if (items.Count < itemsMax)
                            {
                                // another item?
                                Console.WriteLine("Do you want to enter info for another item (y/n)");
                                do
                                {
                                    readResult = Console.ReadLine();
                                    if (readResult != null)
                                    {
                                        addAnotherItem = readResult.ToLower();
                                    }

                                } while (addAnotherItem != "y" && addAnotherItem != "n");
                            }
                        }

                        if (items.Count >= itemsMax)
                        {
                            Console.WriteLine("We have reached our limit on the number of items that we can manage.");
                            Console.WriteLine("Press the Enter key to continue.");
                            readResult = Console.ReadLine();
                        }

                        break;

                    case "3":
                        // Ensure animal ages and physical descriptions are complete
                        Console.WriteLine("Challenge Project - please check back soon to see progress.");
                        Console.WriteLine("Press the Enter key to continue.");
                        readResult = Console.ReadLine();
                        break;

                    case "2":
                        // Ensure animal nicknames and personality descriptions are complete
                        Console.WriteLine("Challenge Project - please check back soon to see progress.");
                        Console.WriteLine("Press the Enter key to continue.");
                        readResult = Console.ReadLine();
                        break;

                    case "5":
                        // Edit an animal's age");
                        Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                        Console.WriteLine("Press the Enter key to continue.");
                        readResult = Console.ReadLine();
                        break;

                    case "6":
                        // Edit an animal's personality description");
                        Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                        Console.WriteLine("Press the Enter key to continue.");
                        readResult = Console.ReadLine();
                        break;

                    case "7":
                        // Display all cats with a specified characteristic
                        Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                        Console.WriteLine("Press the Enter key to continue.");
                        readResult = Console.ReadLine();
                        break;

                    case "8":
                        // Display all dogs with a specified characteristic
                        Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                        Console.WriteLine("Press the Enter key to continue.");
                        readResult = Console.ReadLine();
                        break;

                    default:
                        break;
                }

            } while (menuSelection != "-1");

        } while (menuSelection != "exit");
    }
}

public interface IItem
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    void GetItemDetails();
}
class Item : IItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; set; }
    public Item(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
    public virtual void GetItemDetails()
    {
        Console.WriteLine($"Item Name: {Name} Price: {Price}");
    }
    
}
class ItemWithCategory : Item
{
    public string Category { get; private set; }
    public ItemWithCategory(int id, string name, decimal price, string category) : base(id, name, price)
    {
        Category = category;
    }
}

class BookItem : ItemWithCategory
{
    public string Author { get; private set; }
    public BookItem(int id, string name, decimal price, string category, string author) : base(id, name, price, category)
    {
        Author = author;
    }
    public override void GetItemDetails()
    {
        Console.WriteLine($"Category: {Category} Name: {Name} Author: {Author} Price: {Price}");
    }
}
class ClothingItem : ItemWithCategory
{
    public string Size { get; set; }
    public string Color { get; set; }
    public ClothingItem(int id, string name, decimal price, string category, string size, string color) : base(id, name, price, category)
    {
        Size = size;
        Color = color;
    }
    public override void GetItemDetails()
    {
        Console.WriteLine($"Category: {Category} Name: {Name} Size: {Size} Color: {Color} Price: {Price}");
    }
}
class ElectronicItem : ItemWithCategory
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public ElectronicItem(int id, string name, decimal price, string category, string brand, string model) : base(id, name, price, category)
    {
        Brand = brand;
        Model = model;
    }
    public override void GetItemDetails()
    {
        Console.WriteLine($"Category: {Category} Name: {Name} Brand: {Brand} Model: {Model} Price: {Price}");
    }
}






