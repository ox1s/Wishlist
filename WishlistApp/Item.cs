class Item : IItem
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; private set; }
    public bool IsPurchased { get; private set; }
    public Item(int id, string name, decimal price, bool isPurchased = false)
    {
        Id = id;
        Name = name;
        Price = price;
        IsPurchased = isPurchased;
    }
    
    
    public void MarkAsPurchased()
    {
        IsPurchased = true;
    }
    public void ChangePrice(decimal newPrice)
    {
        Price = newPrice;
    }


}