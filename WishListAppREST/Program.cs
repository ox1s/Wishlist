using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

var itemsDictionary = new ConcurrentDictionary<int, Item>();
builder.Services.AddSingleton(itemsDictionary);
builder.Services.AddScoped<IItemService, ItemService>(); 

var app = builder.Build();

app.MapGet("/api/items", (IItemService itemService) => itemService.GetAllItems());

app.MapGet("/api/items/{id}", (int id, IItemService itemService) =>
{
    var item = itemService.GetItem(id);
    return item != null ? TypedResults.Ok(item) : Results.NotFound();
});

app.MapPost("/api/items/{id}", (int id, Item item, IItemService itemService) =>
{
    return itemService.AddItem(id, item)
        ? TypedResults.Created($"/api/items/{id}", item)
        : Results.BadRequest(new { id = "A item with this id already exists" });
});

app.MapPut("/api/items/{id}", (int id, Item item, IItemService itemService) =>
{
    return itemService.UpdateItemStatus(id, item)
        ? Results.NoContent()
        : Results.NotFound();
});

app.MapDelete("/api/items/{id}", (int id, IItemService itemService) =>
{
    return itemService.RemoveItem(id)
        ? Results.NoContent()
        : Results.NotFound();
});



app.Run();
record Item(string Name, decimal Price, bool IsPurchased);

interface IItemService
{
    IEnumerable<Item> GetAllItems();
    Item GetItem(int id);
    bool AddItem(int id, Item item);
    bool UpdateItemStatus(int id, Item item);
    bool RemoveItem(int id);
}

class ItemService : IItemService
{
    private readonly ConcurrentDictionary<int, Item> _items;

    public ItemService(ConcurrentDictionary<int, Item> items)
    {
        _items = items;
    }

    public IEnumerable<Item> GetAllItems() => _items.Values;

    public Item GetItem(int id) => _items.TryGetValue(id, out var item) ? item : default!;
    public bool AddItem(int id, Item item) => _items.TryAdd(id, item);

    public bool UpdateItemStatus(int id, Item item)
    {
        if (_items.ContainsKey(id))
        {
            _items[id] = item;
            return true;
        }
        return false;
    }

    public bool RemoveItem(int id) => _items.TryRemove(id, out _);
}