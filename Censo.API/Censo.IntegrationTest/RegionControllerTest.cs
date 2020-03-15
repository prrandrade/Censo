namespace Censo.IntegrationTest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using API;
    using Domain.Model;
    using Infra.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;
    using Xunit;

    public class RegionControllerTest
    {
        private readonly HttpClient _client;
        private readonly DatabaseContext _context;

        public RegionControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTest")
                .UseStartup<Startup>());

            _client = server.CreateClient();
            _context = server.Host.Services.GetService(typeof(DatabaseContext)) as DatabaseContext;
        }

        [Fact]
        public async Task GetAllRegions()
        {
            // arrange
            var model = new List<RegionModel>
            {
                new RegionModel {Id = 1, Value = "Norte"},
                new RegionModel {Id = 2, Value = "Sul"},
                new RegionModel {Id = 3, Value = "Sudeste"}
            };
            _context.Regions.AddRange(model);
            await _context.SaveChangesAsync();

            // act
            var response = await _client.GetAsync("/api/census/region");

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<List<RegionModel>>(await response.Content.ReadAsStringAsync()).OrderBy(x => x.Id).ToList();

            Assert.Equal(model.Count, result.Count);
            Assert.Equal(model[0].Id, result[0].Id);
            Assert.Equal(model[1].Id, result[1].Id);
            Assert.Equal(model[2].Id, result[2].Id);
        }
    }
}
