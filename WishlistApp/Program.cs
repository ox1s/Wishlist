
using System.Drawing;

interface IItem
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
        Console.WriteLine($"{Id}. {Name} \n\tPrice: {Price}");
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
        Console.WriteLine($"{Id}. {Name}:\n\tCategory: {Category}\n\tAuthor: {Author}\n\tPrice: {Price}");
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
        Console.WriteLine($"{Id}. {Name}:\n\tCategory: {Category}\n\tSize: {Size}\n\tColor: {Color}\n\tPrice: {Price}");
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
        Console.WriteLine($"{Id}. {Name}\n\tCategory: {Category}\n\tBrand: {Brand}\n\tModel: {Model}\n\tPrice: {Price}");
    }
}
/* interface IComand
{
    void Execute();
}
 */
class Program
{
    static void Main(string[] args)
    {

        int itemsMax = int.MaxValue;
        string? readResult;
        List<Item> items = new List<Item>();

        //Console.BackgroundColor = ConsoleColor.White;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ConsoleKeyInfo key;
        int option = 1;
        bool isSelected = false;
        string green = " \u001b[32m >  ";
        string red = "\u001b[31m x  ";

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            /* Console.WriteLine(@"
 ╦ ╦┬┌─┐┬ ┬╦  ┬┌─┐┌┬┐
 ║║║│└─┐├─┤║  │└─┐ │ 
 ╚╩╝┴└─┘┴ ┴╩═╝┴└─┘ ┴ 
  ");
             Console.WriteLine(@"                                                         
 ,--.   ,--.,--.       ,--.     ,--.   ,--.        ,--.   
 |  |   |  |`--' ,---. |  ,---. |  |   `--' ,---.,-'  '-. 
 |  |.'.|  |,--.(  .-' |  .-.  ||  |   ,--.(  .-''-.  .-' 
 |   ,'.   ||  |.-'  `)|  | |  ||  '--.|  |.-'  `) |  |   
 '--'   '--'`--'`----' `--' `--'`-----'`--'`----'  `--'   
                                                          ");
             Console.WriteLine(@" __    __ _     _       __ _     _   
 / / /\ \ (_)___| |__   / /(_)___| |_ 
 \ \/  \/ / / __| '_ \ / / | / __| __|
  \  /\  /| \__ \ | | / /__| \__ \ |_ 
   \/  \/ |_|___/_| |_\____/_|___/\__|
                                      ");*/
            Console.WriteLine(@"
 ____      ____  _          __       _____      _          _    
|_  _|    |_  _|(_)        [  |     |_   _|    (_)        / |_  
  \ \  /\  / /  __   .--.   | |--.    | |      __   .--. `| |-' 
   \ \/  \/ /  [  | ( (`\]  | .-. |   | |   _ [  | ( (`\] | |   
    \  /\  /    | |  `'.'.  | | | |  _| |__/ | | |  `'.'. | |,  
     \/  \/    [___][\__) )[___]|__]|________|[___][\__) )\__/  
                                                                
");
            Console.ResetColor();
            Console.WriteLine("\nUse up and down to navigate and press the \u001b[32mEnter/Return\u001b[0m key to select");
            (int left, int top) = Console.GetCursorPosition();

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.CursorVisible = false;
                Console.WriteLine($"{(option == 1 ? green : "     ")}1. Add a new item to our list\u001b[0m");
                Console.WriteLine($"{(option == 2 ? green : "     ")}2. Make as purchased\u001b[0m");
                Console.WriteLine($"{(option == 3 ? green : "     ")}3. Set price to item\u001b[0m");
                Console.WriteLine($"{(option == 4 ? green : "     ")}4. List all of our items\u001b[0m");
                Console.WriteLine($"{(option == 5 ? red : "    ")}Exit\u001b[0m");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 5 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 5 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }
            
            Console.Clear();
            Console.CursorVisible = true;

            switch (option)
            {

                case 1:
                    string addAnotherItem = "y";
                    
                    while (addAnotherItem == "y" && items.Count < itemsMax)
                    {
                        bool validEntry = false;
                        string? input = null;
                        do
                        {
                            Console.WriteLine("\rPress Enter to select category of item or enter 'n' if this is not necessary");
                            readResult = Console.ReadLine();
                            input = readResult.Trim().ToLower();
                            if (input == "n" || input == "")
                            {
                                validEntry = true;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"You entered: {input}.");
                                validEntry = false;
                            }
                        } while (!validEntry);

                        do
                        {
                            if (input != "n")
                            {
                                string? category;
                                Console.Clear();
                                Console.WriteLine("Enter category\n\t - el(for electronic)\n\t - c(for clothes)\n\t - b(for books)");
                                readResult = Console.ReadLine();
                                if (readResult != null)
                                {
                                    category = readResult;
                                    if (category != "n")
                                    {
                                        switch (category)
                                        {
                                            case "el":
                                                Console.Clear();
                                                Console.WriteLine("Enter the name of the electronic");
                                                readResult = Console.ReadLine();
                                                if (readResult != null)
                                                {
                                                    string? electronicName = readResult;
                                                    string model = "-";
                                                    string brand = "-";
                                                    if (electronicName != null)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"Do you want to enter Model and Brand of the {electronicName}?(y/n)");
                                                        readResult = Console.ReadLine();
                                                        if (readResult != null)
                                                        {
                                                            string? answer = readResult;
                                                            if (answer == "y")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Enter Model of the electronic");
                                                                readResult = Console.ReadLine();
                                                                if (readResult != null)
                                                                {
                                                                    model = readResult;
                                                                    Console.Clear();
                                                                    Console.WriteLine("Enter Brand of the electronic");
                                                                    readResult = Console.ReadLine();
                                                                    if (readResult != null)
                                                                    {
                                                                        brand = readResult;
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        do
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine($"Enter the price of the {electronicName}");
                                                            readResult = Console.ReadLine();

                                                            if (decimal.TryParse(readResult, out decimal electronicPrice))
                                                            {
                                                                Console.WriteLine("Работает");
                                                                ElectronicItem electronicItem = new ElectronicItem(items.Count + 1, electronicName, electronicPrice, category, brand, model);
                                                                items.Add(electronicItem);
                                                                Console.Clear();
                                                                Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                                                electronicItem.GetItemDetails();
                                                                Console.ReadKey();
                                                                validEntry = true;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine($"{red}Invalid entry!{green} Please try again.(Press Enter)\u001b[0m");
                                                                Console.ReadKey();
                                                                validEntry = false;
                                                            }
                                                        } while (!validEntry);
                                                    }
                                                }
                                                break;
                                            case "c":
                                                Console.Clear();
                                                Console.WriteLine("Enter the name of the clothes");
                                                readResult = Console.ReadLine();
                                                if (readResult != null)
                                                {
                                                    string? clothingName = readResult;
                                                    if (clothingName != null)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"Enter the price of the {clothingName}");
                                                        readResult = Console.ReadLine();
                                                        if (decimal.TryParse(readResult, out decimal clothingPrice))
                                                        {
                                                            ClothingItem clothingItem = new ClothingItem(items.Count + 1, clothingName, clothingPrice, category, "X", "blue");
                                                            items.Add(clothingItem);
                                                            Console.Clear();
                                                            Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                                            clothingItem.GetItemDetails();
                                                            Console.ReadKey();
                                                            validEntry = true;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "b":
                                                Console.Clear();
                                                Console.WriteLine("Enter the name of the book");
                                                readResult = Console.ReadLine();
                                                if (readResult != null)
                                                {
                                                    string? bookName = readResult;
                                                    if (bookName != null)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"Enter the price of the {bookName}");
                                                        readResult = Console.ReadLine();
                                                        if (decimal.TryParse(readResult, out decimal bookPrice))
                                                        {
                                                            BookItem bookItem = new BookItem(items.Count + 1, bookName, bookPrice, category, "Author");
                                                            items.Add(bookItem);
                                                            Console.Clear();
                                                            Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                                            bookItem.GetItemDetails();
                                                            Console.ReadKey();
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
                                Console.Clear();
                                Console.WriteLine("Enter the name of item");
                                readResult = Console.ReadLine();
                                if (readResult != null)
                                {
                                    string? itemName = readResult;
                                    if (itemName != null)
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Enter the price of the {itemName}");
                                        readResult = Console.ReadLine();
                                        if (decimal.TryParse(readResult, out decimal itemPrice))
                                        {
                                            Item item = new Item(items.Count + 1, itemName, itemPrice);
                                            items.Add(item);
                                            Console.Clear();
                                            Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                            item.GetItemDetails();
                                            Console.ReadKey();
                                            validEntry = true;
                                        }
                                    }
                                }
                            }
                        } while (!validEntry);


                        if (items.Count < itemsMax)
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Do you want to enter info for another item (y/n)");
                                readResult = Console.ReadLine();
                                if (readResult != null)
                                {
                                    addAnotherItem = readResult.ToLower();
                                }
                            } while (addAnotherItem != "y" && addAnotherItem != "n");
                            Console.Clear();
                        }
                    }
                    if (addAnotherItem == "n")
                    {
                        Console.Clear();
                        isSelected = false;
                    }
                    if (items.Count >= itemsMax)
                    {
                        Console.Clear();
                        Console.WriteLine("We have reached our limit on the number of items that we can manage.");
                        Console.WriteLine("Press the Enter key to continue.");
                        Console.ReadKey();
                    }

                    break;
                case 2:
                    // Logic for marking an item as purchased
                    Console.WriteLine("2");
                    Console.WriteLine("Press the Enter key to continue.");
                    Console.ReadKey();
                    isSelected = false;
                    break;
                case 3:
                    // Logic for setting the price of an item
                    Console.WriteLine("3");
                    Console.WriteLine("Press the Enter key to continue.");
                    Console.ReadKey();
                    isSelected = false;
                    break;

                case 4:
                    //4. List all of our items
                    foreach (var item in items)
                    {
                        item.GetItemDetails();
                    }
                    Console.WriteLine("\n\rPress any key to continue");
                    Console.ReadKey();
                    isSelected = false;
                    break;
            }


        } while (option != 5);
    }
}