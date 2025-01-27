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