using Microsoft.EntityFrameworkCore;
using Module18.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace Module18.Test
{
    public class ProductsControllerTest
    {
        private readonly Client _client;
        private readonly NorthwindContext _context;

        public ProductsControllerTest()
        {
            var httpClient = new HttpClient();
            _client = new Client("https://localhost:7069/", httpClient);
            _context = new NorthwindContext();
        }

        [Fact]
        public async void GetProducts_ReturnsAllProducts()
        {
            var response = await _client.ProductsAllAsync();
            Assert.Equal(await _context.Products.CountAsync(), response.Result.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(9)]
        public async void GetProduct_ProductId_ReturnsProduct(int id)
        {
            var response = await _client.ProductsGETAsync(id);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async void PostProduct_Product()
        {
            var response = await _client.ProductsPOSTAsync(new Product
            {
                ProductName = "test",
                UnitPrice = 1235,
                CategoryId = 2
            });
            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async void PutProduct_Product()
        {
            var product = await _context.Products.FindAsync(1);
            var response = await _client.ProductsPUTAsync(product.ProductId, new Product
            {
                ProductId = product.ProductId,
                ProductName = "test",
                UnitPrice = 123
            });
            Assert.Equal(200, response.StatusCode);
        }
    }
}