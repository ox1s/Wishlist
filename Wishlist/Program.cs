using System.Data.Common;
using System.Linq.Expressions;
using System.Security;

class Program
{
    static void Main(string[] args)
    {
        List<IItem> items = new List<IItem>();
        Book book = new Book();
        book.Id = 1;
        book.Name = "The Alchemist";
        book.Author = "Paulo Coelho";
        book.Price = 100;
        book.GetItemDetails();
        items.Add(book);
        do
        {
            // display the top-level menu options
            Console.Clear();

            Console.WriteLine("Welcome to the Wishlist app. Your main menu options are:");
            Console.WriteLine(" 1. Add a new item to our list");
            Console.WriteLine(" 2. Make as purchased");
            Console.WriteLine(" 3. List all of our items");
            Console.WriteLine(" 4. Set price to item");
            Console.WriteLine(" 5. Edit an animal’s age");
            Console.WriteLine(" 6. Edit an animal’s personality description");
            Console.WriteLine(" 7. Display all cats with a specified characteristic");
            Console.WriteLine(" 8. Display all dogs with a specified characteristic");
            Console.WriteLine();
            Console.WriteLine("Enter your selection number (or type Exit to exit the program)");


            int itemsMax = int.MaxValue;
            string? readResult = Console.ReadLine();
            int menuSelection = -1;
            if (readResult != null && int.TryParse(readResult, out menuSelection))
            {
                menuSelection = int.Parse(readResult.ToLower());
            }

            switch (menuSelection)
            {
                case 1:
                    foreach(var item in items)
                    {
                        item.GetItemDetails();
                    }
                    Console.WriteLine("\n\rPress the Enter key to continue");
                    readResult = Console.ReadLine();

                    break;

                case 2:
                    string anotherItem = "y";
                    
                    while (anotherItem == "y" && items.Count < itemsMax)
                    {
                        bool validEntry = false;

                        do
                        {
                            Console.WriteLine("\n\rEnter 'category' or 'cat' to begin a new entry");
                            readResult = Console.ReadLine();
                            string? input = readResult;
                            if (readResult != null)
                            {
                                input = readResult.ToLower();
                                if (input != "category" || input != "cat")
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
                            string? category;
                            Console.WriteLine("Enter categoty\n- el(for electronic)\n- c(for clothes)\n - b(for books) or enter 'n' if not necessary");
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
                                                Console.WriteLine("Enter the price of the electronic");
                                                readResult = Console.ReadLine();
                                                if (readResult != null)
                                                {
                                                    decimal electronicPrice = decimal.Parse(readResult);
                                                    ElectronicItem electronicItem = new ElectronicItem();
                                                    electronicItem.Id = items.Count + 1;
                                                    electronicItem.Name = electronicName;
                                                    electronicItem.Price = electronicPrice;
                                                    items.Add(electronicItem);
                                                }
                                            }
                                            break;
                                        case "c":
                                           Console.WriteLine("Enter the name of the clothes");
                                            readResult = Console.ReadLine();
                                            if (readResult != null)
                                            {
                                                string? clothingName = readResult;
                                                Console.WriteLine("Enter the price of the clothes");
                                                readResult = Console.ReadLine();
                                                if (readResult != null)
                                                {
                                                    decimal clothingPrice = decimal.Parse(readResult);
                                                    ClothingItem clothingItem = new ClothingItem();
                                                    clothingItem.Id = items.Count + 1;
                                                    clothingItem.Name = clothingName;
                                                    clothingItem.Price = clothingPrice;
                                                    items.Add(clothingItem);
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
                        } while (validEntry == false);

                       
                        do
                        {
                            Console.WriteLine("Enter a physical description of the pet (size, color, gender, weight, housebroken)");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                animalPhysicalDescription = readResult.ToLower();
                                if (animalPhysicalDescription == "")
                                {
                                    animalPhysicalDescription = "tbd";
                                }
                            }
                        } while (animalPhysicalDescription == "");

                        // get a description of the pet's personality - animalPersonalityDescription can be blank.
                        do
                        {
                            Console.WriteLine("Enter a description of the pet's personality (likes or dislikes, tricks, energy level)");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                animalPersonalityDescription = readResult.ToLower();
                                if (animalPersonalityDescription == "")
                                {
                                    animalPersonalityDescription = "tbd";
                                }
                            }
                        } while (animalPersonalityDescription == "");

                        // get the pet's nickname. animalNickname can be blank.
                        do
                        {
                            Console.WriteLine("Enter a nickname for the pet");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                animalNickname = readResult.ToLower();
                                if (animalNickname == "")
                                {
                                    animalNickname = "tbd";
                                }
                            }
                        } while (animalNickname == "");

                        // store the pet information in the ourAnimals array (zero based)
                        ourAnimals[petCount, 0] = "ID #: " + animalID;
                        ourAnimals[petCount, 1] = "Species: " + animalSpecies;
                        ourAnimals[petCount, 2] = "Age: " + animalAge;
                        ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
                        ourAnimals[petCount, 4] = "Physical description: " + animalPhysicalDescription;
                        ourAnimals[petCount, 5] = "Personality: " + animalPersonalityDescription;

                        // increment petCount (the array is zero-based, so we increment the counter after adding to the array)
                        petCount = petCount + 1;

                        // check maxPet limit
                        if (petCount < maxPets)
                        {
                            // another pet?
                            Console.WriteLine("Do you want to enter info for another pet (y/n)");
                            do
                            {
                                readResult = Console.ReadLine();
                                if (readResult != null)
                                {
                                    anotherPet = readResult.ToLower();
                                }

                            } while (anotherPet != "y" && anotherPet != "n");
                        }
                    }

                    if (petCount >= maxPets)
                    {
                        Console.WriteLine("We have reached our limit on the number of pets that we can manage.");
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

                case "4":
                    // Ensure animal nicknames and personality descriptions are complete
                    Console.WriteLine("Challenge Project - please check back soon to see progress.");
                    Console.WriteLine("Press the Enter key to continue.");
                    readResult = Console.ReadLine();
                    break;

                case "5":
                    // Edit an animal’s age");
                    Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                    Console.WriteLine("Press the Enter key to continue.");
                    readResult = Console.ReadLine();
                    break;

                case "6":
                    // Edit an animal’s personality description");
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
abstract class Item : IItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public virtual void GetItemDetails()
    {
        Console.WriteLine($"Item Name: {Name} Price: {Price}");
    }


}
class BookItem : Item
{
    public string Author { get; set; }
    public override void GetItemDetails()
    {
        Console.WriteLine($"Book Name: {Name} Author: {Author} Price: {Price}");
    }
}
class ClothingItem : Item
{
    public string Size { get; set; }
    public string Color { get; set; }
    public override void GetItemDetails()
    {
        Console.WriteLine($"Clothing Item Name: {Name} Size: {Size} Color: {Color} Price: {Price}");
    }
}
class ElectronicItem : Item
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public override void GetItemDetails()
    {
        Console.WriteLine($"Electronic Item Name: {Name} Brand: {Brand} Model: {Model} Price: {Price}");
    }
}






