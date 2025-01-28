class Item : IItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; set; }
    public bool IsPurchased { get; set; }
    public Item(int id, string name, decimal price, bool isPurchased = false)
    {
        Id = id;
        Name = name;
        Price = price;
        IsPurchased = isPurchased;
    }
    
    public virtual void GetItemDetails()
    {
        Console.WriteLine($"{Id}. {Name} \n\tPrice: {Price}\n\tIs Purchased: {(IsPurchased ? "Yes" : "No")}");
    }
    public void MarkAsPurchased()
    {
        IsPurchased = true;
    }


}