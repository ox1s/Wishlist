using System.Collections.Concurrent;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<IItem> _items = new List<IItem>();


app.MapGet("/items", () => _items);
app.MapGet("/items/{id}", (string id) =>
{
    if (!int.TryParse(id, out int intId) || intId <= 0)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]> 
        { 
            { nameof(id), new string[] { "Id must be a number greater than 0" } } 
        });
    } 

    var item = _items.FirstOrDefault(i => i.Id == intId);
    return item != null 
        ? Results.Ok(item) 
        : Results.NotFound();
});


app.Run();
