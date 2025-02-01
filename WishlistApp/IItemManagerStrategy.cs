interface IItemManagerStrategy
{
    void Display(IItem item);
    void DisplayAllItems(List<IItem> items);
}
class SimpleDisplayStrategy : IItemManagerStrategy
{
    public void Display(IItem item)
    {
        Console.WriteLine($"{item.Id}. {item.Name} Is Purchased: {(item.IsPurchased ? "Yes" : "No")}");
    }
    public void DisplayAllItems(List<IItem> items)
    {
        foreach (var item in items)
        {
            Display(item);
        }
    }
}
class PriceDisplayStrategy : IItemManagerStrategy
{
    public void Display(IItem item)
    {
        Console.WriteLine($"{item.Id}. {item.Name} Price: {item.Price}");
    }
    public void DisplayAllItems(List<IItem> items)
    {
        foreach (var item in items)
        {
            Display(item);
        }
    }
}
class DisplayStrategyWithoutPurсhase : IItemManagerStrategy
{
    public void Display(IItem item)
    {
        if (item is BookItem book)
        {
            Console.WriteLine($"{item.Id}. {item.Name}:\n\tCategory: Books\n\tAuthor: {book.Author}{(item.Price != 0 ? $"\n\tPrice: {item.Price} c.u." : "")}");
        }
        else if (item is ElectronicItem electronic)
        {
            Console.WriteLine($"{item.Id}. {item.Name}:\n\tCategory: Electronic {electronic.Brand} {(electronic.Brand != "-" ? $"\n\tBrand: {electronic.Brand}" : "")}{(electronic.Brand != "-" ? $"\n\tModel: {electronic.Model}" : "")}{(item.Price != 0 ? $"\n\tPrice: {item.Price} c.u." : "")}");
        }
        else if (item is ClothingItem clothing)
        {
            Console.WriteLine($"{item.Id}. {item.Name}:\n\tCategory: Clothes{(clothing.Size != "-" ? $"\n\tSize: {clothing.Size}" : "")}{(clothing.Color != "-" ? $"\n\t\n\tColor: {clothing.Color}" : "")}{(item.Price != 0 ? $"\n\tPrice: {item.Price} c.u." : "")}");
        }
        else
        {
            Console.WriteLine($"{item.Id}. {item.Name}{(item.Price != 0 ? $":\n\tPrice: {item.Price} c.u." : "")}");
        }
    }
    public void DisplayAllItems(List<IItem> items)
    {
        foreach (var item in items)
        {
            Display(item);
        }
    }
}
class DetailedDisplayStrategy : IItemManagerStrategy
{
    public void Display(IItem item)
    {
        if (item is BookItem book)
        {
            Console.WriteLine($"{item.Id}. {item.Name}:\n\tCategory: Books\n\tAuthor: {book.Author}\n\tPrice: {item.Price} c.u.\n\tIs Purchased: {(item.IsPurchased ? "Yes" : "No")}");
        }
        else if (item is ElectronicItem electronic)
        {
            Console.WriteLine($"{item.Id}. {item.Name}\n\tCategory: Electronic\n\tBrand: {electronic.Brand}\n\tModel: {electronic.Model}\n\tPrice: {item.Price} c.u.\n\tIs Purchased: {(item.IsPurchased ? "Yes" : "No")}");
        }
        else if (item is ClothingItem clothing)
        {
            Console.WriteLine($"{item.Id}. {item.Name}\n\tCategory: Clothes\n\tSize: {clothing.Size}\n\tColor: {clothing.Color}\n\tPrice: {item.Price} c.u.\n\tIs Purchased: {(item.IsPurchased ? "Yes" : "No")}");
        }
        else
        {
            Console.WriteLine($"{item.Id}. {item.Name} \n\tPrice: {item.Price} c.u.\n\tIs Purchased: {(item.IsPurchased ? "Yes" : "No")}");
        }
    }
    public void DisplayAllItems(List<IItem> items)
    {
        foreach (var item in items)
        {
            Display(item);
        }
    }
}