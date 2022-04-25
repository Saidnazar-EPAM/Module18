using Microsoft.EntityFrameworkCore;
using Module18.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace Module18.Test
{
    public class CategoriesControllerTest
    {
        private readonly Client _client;
        private readonly NorthwindContext _context;

        public CategoriesControllerTest()
        {
            var httpClient = new HttpClient();
            _client = new Client("https://localhost:7069/", httpClient);
            _context = new NorthwindContext();
        }

        [Fact]
        public async void GetCategories_ReturnsAllCategories()
        {
            var response = await _client.CategoriesAsync();
            Assert.Equal(await _context.Categories.CountAsync(), response.Result.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(8)]
        public async void GetPicture_CategoryId_ReturnsPictureOfCategory(int id)
        {
            await _client.GetPictureAsync(id);
        }

        [Fact]
        public async void UpdatePicture_PictureFile()
        {
            using var stream = new FileStream("test.jpg", FileMode.Open);
            var response = await _client.UpdatePictureAsync(1, new FileParameter(stream));
        }
    }
}