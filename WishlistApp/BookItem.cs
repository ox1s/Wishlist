class BookItem : Item
{
    public string Author { get; private set; }
    public BookItem(int id, string name, decimal price, string author) : base(id, name, price)
    {
        Author = author;
    }
    public override void GetItemDetails()
    {
        Console.WriteLine($"{Id}. {Name}:\n\tCategory: Books\n\tAuthor: {Author}\n\tPrice: {Price}");
    }
}