class ClothingItem : Item
{
    public string Size { get; set; }
    public string Color { get; set; }
    public ClothingItem(int id, string name, decimal price, string size, string color) : base(id, name, price)
    {
        Size = size;
        Color = color;
    }
    public override void GetItemDetails()
    {
        Console.WriteLine($"{Id}. {Name}:\n\tCategory: Clothing\n\tSize: {Size}\n\tColor: {Color}\n\tPrice: {Price}\n\tIs Purchased: {(IsPurchased ? "Yes" : "No")}");
    }
}