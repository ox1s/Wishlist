using System;
using System.Collections.Generic;
using System.Net;
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
                    Console.CursorVisible = true;
                    SetPrice(items);
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
                response = GetAddAnotherItemResponse("Do you want to enter info for another item (y/n)");
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
                AddItemByType(items, "electronic");
                break;
            case "c":
                AddItemByType(items, "clothing");
                break;
            case "b":
                AddItemByType(items, "book");
                break;
            case "n":
                AddItemByType(items, null);
                break;
            default:
                Console.WriteLine($"{red}Invalid entry!\u001b[0m \nPlease try again(Press Enter).");
                Console.ReadKey();
                validEntry = false;
                break;
        }
    }
    private string? GetAddAnotherItemResponse(string question, Func<List<IItem>>? title = null)
    {
        do
        {
            Console.Clear();
            if (title != null) title();
            Console.WriteLine(question);
            readResult = Console.ReadLine();
            if (readResult != "y" && readResult != "n")
            {
                Console.WriteLine($"You entered: {readResult}.");
                Console.WriteLine("Invalid input. Please try again(Press Enter).");
                Console.ReadKey();
            }
            Console.Clear();
        } while (readResult != "y" && readResult != "n");

        return readResult.ToLower();
    }
    public void MarkAsPurchased(List<IItem> items)
    {
        // Логика для пометки предмета как купленного
        do
        {
            UpdatePurchaseStatus(items);
            if (items.Count == 0 || items.All(item => item.IsPurchased))
            {
                Console.WriteLine("You don't have any items to purchase.");
                break;
            }
            else
            {
                Console.WriteLine("Enter the ID of the item you want to mark as or press Enter to cancel:");
                readResult = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(readResult))
                {
                    decimal id = -1;
                    decimal.TryParse(readResult, out id);
                    if (id != -1)
                    {
                        var item = items.Find(i => i.Id == id);
                        if (item != null)
                        {
                            if (!item.IsPurchased)
                            {
                                item.MarkAsPurchased();
                                UpdatePurchaseStatus(items);
                                Console.WriteLine($"Item {item.Name} has been marked as purchased. Press Enter to continue.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine($"Item {item.Name} is already marked as purchased. Press Enter to continue.");
                                Console.ReadKey();
                            }

                            if (!items.All(item => item.IsPurchased))
                            {
                                string? response = GetAddAnotherItemResponse("Do you want to mark another item as purchased? (y/n)", () =>
                                {
                                    UpdatePurchaseStatus(items);
                                    return items;
                                });

                                if (response == "n")
                                {
                                    UpdatePurchaseStatus(items);
                                    break;
                                }
                            }
                            else
                            {
                                readResult = null;
                            }

                        }
                        else
                        {
                            Console.WriteLine($"Item with ID {id} not found. Please try again.");
                            readResult = null;
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID. Please enter a valid ID.");
                        readResult = null;
                        Console.ReadKey();
                    }
                }
                else
                {
                    break;
                }
            }
        } while (string.IsNullOrEmpty(readResult) || readResult?.ToLower() == "y");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        isSelected = false;
    }
    private void UpdatePurchaseStatus(List<IItem> items)
    {
        Console.Clear();
        itemManager.SwitchDisplayStrategy("simple");
        itemManager.DisplayAllItems(items);
    }
    private void UpdatePriceStatus(List<IItem> items)
    {
        Console.Clear();
        itemManager.SwitchDisplayStrategy("price");
        itemManager.DisplayAllItems(items);
    }
    public void SetPrice(List<IItem> items)
    {
        do
        {
            UpdatePriceStatus(items);
            if (items.Count == 0)
            {
                Console.WriteLine("You don't have any items to set price.");
                break;
            }
            else
            {
                Console.WriteLine("Enter the ID of the product whose price you want to change or press Enter to cancel:");
                readResult = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(readResult))
                {
                    decimal id = -1;
                    decimal.TryParse(readResult, out id);
                    if (id != -1)
                    {
                        var item = items.Find(i => i.Id == id);
                        if (item != null)
                        {
                            do
                            {
                                Console.WriteLine($"Enter the price of the {item.Name}");
                                readResult = Console.ReadLine();
                                if (readResult != null && decimal.TryParse(readResult, out decimal itemPrice) && itemPrice > 0)
                                {

                                    item.ChangePrice(itemPrice);
                                    Console.Clear();
                                    UpdatePriceStatus(items);
                                    Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                    itemManager.SwitchDisplayStrategy("price");
                                    itemManager.DisplayItem(item);
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

                            if (!items.All(item => item.IsPurchased))
                            {
                                string? response = GetAddAnotherItemResponse("Do you want to change another item? (y/n)", () =>
                                {
                                    UpdatePriceStatus(items);
                                    return items;
                                });

                                if (response == "n")
                                {
                                    UpdatePriceStatus(items);
                                    break;
                                }
                            }
                            else
                            {
                                readResult = null;
                            }

                        }
                        else
                        {
                            Console.WriteLine($"Item with ID {id} not found. Please try again.");
                            readResult = null;
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID. Please enter a valid ID.");
                        readResult = null;
                        Console.ReadKey();
                    }
                }
                else
                {
                    break;
                }
            }
        } while (string.IsNullOrEmpty(readResult) || readResult?.ToLower() == "y");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        isSelected = false;
    }
    public void ListItems(List<IItem> items)
    {
        if (items.Count == 0)
        {
            Console.WriteLine("You don't have any items to list.");
        }
        else
        {
            Console.Clear();
            itemManager.SwitchDisplayStrategy("detailed");
            itemManager.DisplayAllItems(items);
        }
        Console.WriteLine("\n\rPress any key to continue");
        Console.ReadKey();
        isSelected = false;
    }

    public void InvalidEntry()
    {
        Console.WriteLine($"{red}Invalid entry!\u001b[0m Please try again.(Press Enter)");
        Console.ReadKey();
        validEntry = false;
    }
    private void AddItemByType(List<IItem> items, string? itemType)
    {
        string author = "";
        string size = "-";
        string color = "-";
        string brand = "-";
        string model = "-";
        string? response;

        do
        {
            Console.Clear();
            Console.WriteLine($"Enter the name of the {(itemType == null ? "" : $"{itemType} ")}item");
            

            readResult = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(readResult))
            {
                string itemName = readResult.Trim();

                do
                {
                    if (itemType == "book")
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Enter the author of the {itemName}");
                            readResult = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(readResult))
                            {
                                author = readResult.Trim();
                                Console.Clear();
                                validEntry = true;
                            }
                            else
                            {
                                InvalidEntry();
                            }
                        } while (!validEntry);

                    }
                    else if (itemType == "clothing")
                    {
                        Console.Clear();
                        response = GetAddAnotherItemResponse($"Do you want to add size to {itemName}? (y/n)");
                        if (response != "n")
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Enter the size of the clothes(You shoud enter XS, S, M, L, XL or XXL)");
                                readResult = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(readResult))
                                {
                                    size = readResult.Trim().ToUpper();
                                    if (size == "XS" || size == "S" || size == "M" || size == "L" || size == "XL" || size == "XXL") validEntry = true;
                                    else
                                    {
                                        InvalidEntry();
                                    }
                                }
                                else
                                {
                                    InvalidEntry();
                                }
                            } while (!validEntry);
                        }
                        Console.Clear();
                        response = GetAddAnotherItemResponse($"Do you want to add color to {itemName}? (y/n)");
                        if (response != "n")
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine($"Enter the color of the {itemName}");
                                readResult = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(readResult))
                                {
                                    color = readResult;
                                    validEntry = true;
                                }
                                else
                                {
                                    InvalidEntry();
                                }
                            } while (!validEntry);
                        }


                    }
                    else if (itemType == "electronic")
                    {
                        Console.Clear();
                        response = GetAddAnotherItemResponse($"Do you want to enter Model and Brand of the {itemName}?(y/n)");
                        if (response != "n")
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Enter Model of the electronic");
                                readResult = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(readResult))
                                {
                                    model = readResult.Trim();
                                    validEntry = true;
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter Brand of the electronic");
                                        readResult = Console.ReadLine();
                                        if (!string.IsNullOrWhiteSpace(readResult))
                                        {
                                            brand = readResult;
                                            validEntry = true;
                                        }
                                        else
                                        {
                                            InvalidEntry();
                                        }
                                    } while (!validEntry);

                                }
                                else
                                {
                                    InvalidEntry();
                                }
                            } while (!validEntry);

                        }
                    }


                    IItem newItem;
                    response = GetAddAnotherItemResponse($"Do you want to add price to {itemName}? (y/n)");
                    if (response == "n")
                    {
                        if (itemType == "clothing")
                        {
                            newItem = new ClothingItem(items.Count + 1, itemName, 0, size, color);
                        }
                        else if (itemType == "electronics")
                        {
                            newItem = new ElectronicItem(items.Count + 1, itemName, 0, brand, model);
                        }
                        else if (itemType == "book")
                        {
                            newItem = new BookItem(items.Count + 1, itemName, 0, author);
                        }
                        else
                        {
                            newItem = new Item(items.Count + 1, itemName, 0);
                        }
                        items.Add(newItem);
                        Console.Clear();
                        Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                        itemManager.SwitchDisplayStrategy("input");
                        itemManager.DisplayItem(newItem);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        validEntry = true;
                        break;
                    }
                    else
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Enter the price of the {itemName}.");
                            readResult = Console.ReadLine();
                            if (readResult != null && decimal.TryParse(readResult, out decimal itemPrice) && itemPrice > 0)
                            {
                                if (itemType == "clothing")
                                {
                                    newItem = new ClothingItem(items.Count + 1, itemName, itemPrice, size, color);
                                }
                                else if (itemType == "electronic")
                                {
                                    newItem = new ElectronicItem(items.Count + 1, itemName, itemPrice, brand, model);
                                }
                                else if (itemType == "book")
                                {
                                    newItem = new BookItem(items.Count + 1, itemName, itemPrice, author);
                                }
                                else
                                {
                                    newItem = new Item(items.Count + 1, itemName, itemPrice);
                                }
                                items.Add(newItem);
                                Console.Clear();
                                Console.WriteLine($"\u001b[34mYou entered:\u001b[0m");
                                itemManager.SwitchDisplayStrategy("input");
                                itemManager.DisplayItem(newItem);
                                Console.WriteLine("Press any key to continue");
                                Console.ReadKey();
                                validEntry = true;
                            }
                            else
                            {
                                InvalidEntry();
                            }
                        } while (!validEntry);
                    }
                } while (!validEntry);

            }
            else
            {
                InvalidEntry();
            }


        } while (!validEntry);
    }

    
}