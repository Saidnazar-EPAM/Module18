using ConsoleClient;
using System.Text.Json;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7069/api/");

var httpResponse = await client.GetAsync("Categories");
var json = await httpResponse.Content.ReadAsStringAsync();
var categories = JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


foreach (var item in categories)
{
    Console.WriteLine($"Name: {item.CategoryName}, Description: {item.Description}");
}

httpResponse = await client.GetAsync("Products");
json = await httpResponse.Content.ReadAsStringAsync();
var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

Console.WriteLine("------------------------------------------------------------");

foreach (var item in products)
{
    Console.WriteLine($"Name: {item.ProductName}, UnitPrice: {item.UnitPrice}, QuantityPerUnit: {item.QuantityPerUnit}");
}
