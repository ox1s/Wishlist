interface IItem
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    public bool IsPurchased { get; }
   
    void MarkAsPurchased();
}