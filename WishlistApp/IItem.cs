interface IItem
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    void GetItemDetails();
}