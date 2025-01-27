using System.Runtime.CompilerServices;

class Menu
{
    string? readResult;
    int itemsMax = int.MaxValue;
    string green = " \u001b[32m >  ";
    string red = "\u001b[31m x  ";
    bool isSelected = false;
    int option = 1;
    //Console.BackgroundColor = ConsoleColor.White;
    ConsoleKeyInfo key;
    bool validEntry = false;


    public void DisplayMenu(List<Item> items)
    {


        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
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

            switch (option)
            {

                case 1:
                    Console.CursorVisible = true;
                    AddItem(items);

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

    public void AddItem(List<Item> items)
    {
        string addAnotherItem = "y";

        while (addAnotherItem == "y" && items.Count < itemsMax)
        {
            string? input = null;
            do
            {
                Console.WriteLine("\rPress Enter to select category of item or enter 'n' if this is not necessary");
                readResult = Console.ReadLine();
                input = readResult?.Trim().ToLower();
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
                string? category;
                if (input != "n")
                {
                    Console.Clear();
                    Console.WriteLine("Enter category\n\t - el(for electronic)\n\t - c(for clothes)\n\t - b(for books)");
                    readResult = Console.ReadLine();
                }
                if (readResult != null)
                {
                    category = readResult?.Trim().ToLower();
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

                                        if (readResult != null && decimal.TryParse(readResult, out decimal electronicPrice))
                                        {
                                            Console.WriteLine("Работает");
                                            ElectronicItem electronicItem = new ElectronicItem(items.Count + 1, electronicName, electronicPrice, brand, model);
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
                                            Console.WriteLine($"{red}Invalid entry!\u001b[0m Please try again.(Press Enter)");
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
                                    if (readResult != null && decimal.TryParse(readResult, out decimal clothingPrice))
                                    {
                                        ClothingItem clothingItem = new ClothingItem(items.Count + 1, clothingName, clothingPrice, "X", "blue");
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
                            AddBookItem(items);
                            break;
                        case "n":
                            AddItemWithoutCategory(items);
                            break;

                        default:
                            Console.WriteLine($"{red}Invalid entry!\u001b[0m Please try again. (Press Enter)");
                            Console.ReadKey();
                            validEntry = false;
                            break;

                    }

                }
            } while (!validEntry);


            if (items.Count < itemsMax)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Do you want to enter info for another item (y/n)");
                    string? readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        addAnotherItem = readResult.ToLower();
                    }
                } while (addAnotherItem != "y" && addAnotherItem != "n");
                if (addAnotherItem == "n")
                {
                    Console.Clear();
                    isSelected = false;
                }
            }
        }
        if (items.Count >= itemsMax)
        {
            Console.Clear();
            Console.WriteLine($"{red}We have reached our limit on the number of items that we can manage!\u001b[0m");
            Console.WriteLine("Press the Enter key to continue.");
            Console.ReadKey();
        }
    }

    public void MarkAsPurchased(List<Item> items)
    {
        // Логика для пометки предмета как купленного
    }

    public void SetPrice(List<Item> items)
    {
        // Логика для изменения цены предмета
    }

    public void ListItems(List<Item> items)
    {
        // Логика для вывода списка предметов
    }


    private void AddItemWithoutCategory(List<Item> items)
    {
        Console.Clear();
        Console.WriteLine("Enter the name of item");
        string? readResult = Console.ReadLine();
        string? itemName = readResult?.Trim();
        if (itemName != null)
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Enter the price of the {itemName}");
                readResult = Console.ReadLine();
                if (readResult != null && decimal.TryParse(readResult, out decimal itemPrice))
                {
                    Item item = new Item(items.Count + 1, itemName, itemPrice);
                    items.Add(item);
                    Console.Clear();
                    Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                    item.GetItemDetails();
                    Console.ReadKey();
                    validEntry = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{red}Invalid entry!\u001b[0m Please try again.(Press Enter)");
                    Console.ReadKey();
                    validEntry = false;
                }
            } while (!validEntry);
        }
        else validEntry = false;
    }
    private void AddBookItem(List<Item> items)
    {
        Console.Clear();
        Console.WriteLine("Enter the name of the book");
        string? bookName = null;
        do
        {
            readResult = Console.ReadLine();
            bookName = readResult?.Trim();
            if (bookName == null)
                Console.WriteLine($"{red}Invalid entry!\u001b[0m Please try again.(Press Enter)");

        } while (bookName == null);

        Console.Clear();
        Console.WriteLine($"Enter the price of the {bookName}");
        readResult = Console.ReadLine();
        if (readResult != null && decimal.TryParse(readResult, out decimal bookPrice))
        {
            BookItem bookItem = new BookItem(items.Count + 1, bookName, bookPrice, "Author");
            items.Add(bookItem);
            Console.Clear();
            Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
            bookItem.GetItemDetails();
            Console.ReadKey();
            validEntry = true;
        }


    }
}