namespace Censo.IntegrationTest
{
    using API;
    using API.ViewModels;
    using Domain.Model;
    using Infra.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class AnswerControllerTest : IDisposable
    {
        private readonly HttpClient _client;
        private readonly DatabaseContext _context;
        private readonly string _address;

        public AnswerControllerTest()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {new KeyValuePair<string, string>("databaseName", "anwser")});

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseConfiguration(builder.Build())
                .UseStartup<Startup>());

            _client = server.CreateClient();
            _context = server.Host.Services.GetService(typeof(DatabaseContext)) as DatabaseContext;
            _address = "/api/census/answer";
        }

        [Fact]
        public async Task Get()
        {
            // arrange
            var modelChild = new AnswerModel
            {
                Id = 1,
                FirstName = "First Name",
                LastName = "Last Name",
                GenderId = 1
            };
            _context.Answer.Add(modelChild);

            var modelParent = new AnswerModel
            {
                Id = 2,
                FirstName = "2First Name",
                LastName = "2Last Name",
                GenderId = 1
            };
            _context.Answer.Add(modelParent);

            var model = new AnswerModel
            {
                Id = 3,
                FirstName = "3First Name",
                LastName = "3Last Name",
                GenderId = 1
            };
            _context.Answer.Add(model);

            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = modelChild, Parent = model });
            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = model, Parent = modelParent });
            await _context.SaveChangesAsync();

            // act
            var response = await _client.GetAsync($"{_address}/3");

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<AnswerViewModel>(await response.Content.ReadAsStringAsync());

            Assert.Equal(model.Id, result.Info.Id);
            Assert.Equal(modelParent.Id, result.ParentsInfo.First().Id);
            Assert.Equal(modelChild.Id, result.ChildrenInfo.First().Id);
        }

        [Fact]
        public async Task GetAll()
        {
            // arrange
            var modelChild = new AnswerModel
            {
                Id = 1,
                FirstName = "First Name",
                LastName = "Last Name",
                GenderId = 1
            };
            _context.Answer.Add(modelChild);

            var modelParent = new AnswerModel
            {
                Id = 2,
                FirstName = "2First Name",
                LastName = "2Last Name",
                GenderId = 1
            };
            _context.Answer.Add(modelParent);

            var model = new AnswerModel
            {
                Id = 3,
                FirstName = "3First Name",
                LastName = "3Last Name",
                GenderId = 1
            };
            _context.Answer.Add(model);

            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = modelChild, Parent = model });
            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = model, Parent = modelParent });
            await _context.SaveChangesAsync();

            // act
            var response = await _client.GetAsync($"{_address}");

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<List<AnswerViewModel>>(await response.Content.ReadAsStringAsync()).OrderBy(x => x.Info.Id).ToList();
            Assert.Equal(modelChild.Id, result[0].Info.Id);
            Assert.Equal(modelParent.Id, result[1].Info.Id);
            Assert.Equal(model.Id, result[2].Info.Id);
        }

        [Fact]
        public async Task Post()
        {
            // arrange
            var model = new AnswerViewModel
            {
                Info = new AnswerInfoViewModel { FirstName = "FirstName", LastName = "LastName" },
                ParentsInfo = new List<AnswerInfoViewModel> { new AnswerInfoViewModel { FirstName = "Parent FirstName", LastName = "Parent LastName" } },
                ChildrenInfo = new List<AnswerInfoViewModel> { new AnswerInfoViewModel { FirstName = "Child FirstName", LastName = "Child LastName" } }
            };

            // act
            var response = await _client.PostAsync(_address, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            
            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<AnswerViewModel>(await response.Content.ReadAsStringAsync());

            Assert.Equal(1, result.Info.Id);
            Assert.Equal(model.Info.FirstName, result.Info.FirstName);

            Assert.Equal(2, result.ParentsInfo.First().Id);
            Assert.Equal(model.ParentsInfo.First().FirstName, result.ParentsInfo.First().FirstName);

            Assert.Equal(3, result.ChildrenInfo.First().Id);
            Assert.Equal(model.ChildrenInfo.First().FirstName, result.ChildrenInfo.First().FirstName);
        }


        public void Dispose()
        {
            _client?.Dispose();
            _context.Database.EnsureDeleted(); // nedded for 'zeroing' the inmemory database between tests
            _context.Dispose();
        }
    }
}
