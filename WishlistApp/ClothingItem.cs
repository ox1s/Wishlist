class ClothingItem : Item
{
    public string Size { get; private set; }
    public string Color { get; private set; }
    public ClothingItem(int id, string name, decimal price, string size, string color) : base(id, name, price)
    {
        Size = size;
        Color = color;
    }
    
}