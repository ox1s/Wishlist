using System;
using System.Collections.Generic;
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
    ItemManager itemManager = new ItemManager();
    public void DisplayMenu(List<IItem> items)
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
                    Console.CursorVisible = true;
                    MarkAsPurchased(items);
                    break;
                case 3:
                    // Logic for setting the price of an item
                    Console.WriteLine("3");
                    Console.WriteLine("Press the Enter key to continue.");
                    Console.ReadKey();
                    isSelected = false;
                    break;

                case 4:
                    ListItems(items);
                    break;
            }
        } while (option != 5);

    }

    public void AddItem(List<IItem> items)
    {
        string? response = "y";

        while (response == "y" && items.Count < itemsMax)
        {
            do
            {
                Console.Clear();
                AddItemByCategory(items, GetItemCategory());
            } while (!validEntry);


            if (items.Count < itemsMax)
            {
                Console.Clear();
                Console.WriteLine("Do you want to enter info for another item (y/n)");
                response = GetAddAnotherItemResponse();
                Console.Clear();
                if (response == "n")
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
    private string? GetItemCategory()
    {
        string? input;
        do
        {
            Console.Clear();
            Console.WriteLine("\rPress Enter to select category of item or enter 'n' if this is not necessary");


            readResult = Console.ReadLine();
            input = readResult?.Trim().ToLower();

            if (input == "n" || string.IsNullOrEmpty(input))
            {
                do
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.Clear();
                        Console.WriteLine("Enter category\n\t - el(for electronic)\n\t - c(for clothes)\n\t - b(for books)");
                        readResult = Console.ReadLine()?.Trim().ToLower();
                        if (readResult != "el" && readResult != "c" && readResult != "b")
                        {
                            Console.WriteLine($"You entered: {readResult}.");
                            Console.WriteLine("Please try again(Press Enter).");
                            Console.ReadKey();
                            validEntry = false;
                        }
                        else
                            validEntry = true;

                    }
                    else
                        validEntry = true;
                } while (!validEntry);
            }
            else
            {
                Console.WriteLine($"You entered: {input}.");
                Console.WriteLine("Please try again(Press Enter).");
                Console.ReadKey();
                validEntry = false;
            }
        } while (!validEntry);
        return input == "n" ? "n" : readResult?.Trim().ToLower();
    }
    private void AddItemByCategory(List<IItem> items, string? category)
    {

        switch (category)
        {
            case "el":
                AddElectronicItem(items);
                break;
            case "c":
                AddClothingItem(items);
                break;
            case "b":
                AddBookItem(items);
                break;
            case "n":
                AddItemWithoutCategory(items);
                break;

            default:
                Console.WriteLine($"{red}Invalid entry!\u001b[0m \nPlease try again(Press Enter).");
                Console.ReadKey();
                validEntry = false;
                break;
        }

    }
    private string? GetAddAnotherItemResponse()
    {
        do
        {
            readResult = Console.ReadLine();
        } while (readResult != "y" && readResult != "n");
        return readResult.ToLower();
    }

    public void MarkAsPurchased(List<IItem> items)
    {
        // Логика для пометки предмета как купленного
        do
        {
            UpdatePurshaseStatus(items);
            Console.WriteLine("Enter the ID of the item you want to mark as purchased:");
            string? readResult = Console.ReadLine();
            if (readResult != null)
            {
                int id = -1;
                int.TryParse(readResult, out id);
                if (id != -1)
                {
                    var item = items.Find(i => i.Id == id);
                    if (item != null)
                    {
                        if (!item.IsPurchased)
                        {
                            item.MarkAsPurchased();
                            Console.WriteLine($"Item {item.Name} has been marked as purchased.");
                            UpdatePurshaseStatus(items);
                        }
                        else
                        {
                            Console.WriteLine($"Item {item.Name} is already marked as purchased.");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Item with ID {id} not found.");
                        Console.ReadKey();
                        readResult = null;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid ID.");
                    Console.ReadKey();
                    readResult = null;
                }
            }
            else
            {
                Console.WriteLine("Do you want to mark another item as purchased? (y/n)");
                readResult = Console.ReadLine();
                if (readResult != null && readResult.ToLower() == "n")
                {
                    break;
                }
            }
        } while (string.IsNullOrEmpty(readResult) || readResult.ToLower() == "y");
        Console.WriteLine("\n\rPress any key to continue");
        Console.ReadKey();
        isSelected = false;
    }
    private void UpdatePurshaseStatus(List<IItem> items)
    {
        Console.Clear();
        itemManager.SwitchDisplayStrategy("simple");
        itemManager.DisplayAllItems(items);
    }

    public void SetPrice(List<Item> items)
    {
        // Логика для изменения цены предмета
    }

    public void ListItems(List<IItem> items)
    {
        Console.Clear();
        itemManager.SwitchDisplayStrategy("detailed");
        itemManager.DisplayAllItems(items);
        Console.WriteLine("\n\rPress any key to continue");
        Console.ReadKey();
        isSelected = false;
    }


    private void AddItemWithoutCategory(List<IItem> items)
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
                    itemManager.SwitchDisplayStrategy("detailed");
                    itemManager.DisplayItem(item);
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
    private void AddBookItem(List<IItem> items)
    {
        Console.Clear();
        Console.WriteLine("Enter the name of the book");
        readResult = Console.ReadLine();
        do
        {
            if (!string.IsNullOrWhiteSpace(readResult))
            {
                validEntry = true;

                string bookName = readResult.Trim();
                string? author = null;

                Console.Clear();
                Console.WriteLine($"Entter Author of the {bookName}");
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    author = readResult;
                    if (author != null)
                    {
                        author = author.Trim();
                        Console.Clear();
                        Console.WriteLine($"Enter the price of the {bookName}");
                        readResult = Console.ReadLine();
                        if (readResult != null && decimal.TryParse(readResult, out decimal bookPrice))
                        {
                            BookItem bookItem = new BookItem(items.Count + 1, bookName, bookPrice, "Author");
                            items.Add(bookItem);
                            Console.Clear();
                            Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                            itemManager.SwitchDisplayStrategy("detailed");
                            itemManager.DisplayItem(bookItem);
                            Console.ReadKey();
                            validEntry = true;
                        }
                    }

                }

            }
        } while (!validEntry);
    }
    private void AddElectronicItem(List<IItem> items)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the electronic");

            readResult = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(readResult))
            {
                string? electronicName = readResult?.Trim();
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

                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Enter the price of the {electronicName} or enter 0 to cancel");
                            readResult = Console.ReadLine();

                            if (readResult != null && decimal.TryParse(readResult, out decimal electronicPrice))
                            {
                                ElectronicItem electronicItem = new ElectronicItem(items.Count + 1, electronicName, electronicPrice, brand, model);
                                items.Add(electronicItem);
                                Console.Clear();
                                Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                itemManager.SwitchDisplayStrategy("detailed");
                                itemManager.DisplayItem(electronicItem);
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
    private void AddClothingItem(List<IItem> items)
    {
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
                    itemManager.SwitchDisplayStrategy("detailed");
                    itemManager.DisplayItem(clothingItem);
                    //clothingItem.GetItemDetails();
                    Console.ReadKey();
                    validEntry = true;
                }
            }
        }
    }
}