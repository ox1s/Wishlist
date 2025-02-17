class ElectronicItem : Item
{
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public ElectronicItem(int id, string name, decimal price, string brand, string model) : base(id, name, price)
    {
        Brand = brand;
        Model = model;
    }
    
}