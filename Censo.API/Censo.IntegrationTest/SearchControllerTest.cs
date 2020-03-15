namespace Censo.IntegrationTest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using API;
    using API.ViewModels;
    using Domain;
    using Domain.Model;
    using Infra.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;
    using Xunit;

    [Collection("Endpoints")]
    public class SearchControllerTest
    {
        private readonly HttpClient _client;
        private readonly DatabaseContext _context;
        private string _address;

        public SearchControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTest")
                .UseStartup<Startup>());

            _client = server.CreateClient();
            _context = server.Host.Services.GetService(typeof(DatabaseContext)) as DatabaseContext;
            _context.Database.EnsureDeleted(); // nedded for 'zeroing' the inmemory database between tests
        }

        [Fact]
        public async Task Genealogy()
        {
            // arrange
            #region Creating Answer with Parent and GrandParent answers

            var model = new AnswerModel
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };
            _context.Answer.Add(model);
            await _context.SaveChangesAsync();

            var parent1Model = new AnswerModel
            {
                FirstName = "Parent1 FirstName",
                LastName = "Parent1 LastName",
            };
            _context.Answer.Add(parent1Model);
            await _context.SaveChangesAsync();

            var parent2Model = new AnswerModel
            {
                FirstName = "Parent2 FirstName",
                LastName = "Parent2 LastName",
            };
            _context.Answer.Add(parent2Model);
            await _context.SaveChangesAsync();

            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = model, Parent = parent1Model });
            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = model, Parent = parent2Model });
            await _context.SaveChangesAsync();

            var grandParent1Model = new AnswerModel
            {
                FirstName = "GrandParent1 FirstName",
                LastName = "GrandParent1 LastName",
            };
            _context.Answer.Add(grandParent1Model);
            await _context.SaveChangesAsync();

            var grandParent2Model = new AnswerModel
            {
                FirstName = "GrandParent2 FirstName",
                LastName = "GrandParent2 LastName",
            };
            _context.Answer.Add(grandParent2Model);
            await _context.SaveChangesAsync();

            var grandParent3Model = new AnswerModel
            {
                FirstName = "GrandParent3 FirstName",
                LastName = "GrandParent3 LastName",
            };
            _context.Answer.Add(grandParent3Model);
            await _context.SaveChangesAsync();

            var grandParent4Model = new AnswerModel
            {
                FirstName = "GrandParent4 FirstName",
                LastName = "GrandParent4 LastName",
            };
            _context.Answer.Add(grandParent4Model);
            await _context.SaveChangesAsync();

            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = parent1Model, Parent = grandParent1Model });
            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = parent1Model, Parent = grandParent2Model });
            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = parent2Model, Parent = grandParent3Model });
            _context.AnswerParentChild.Add(new AnswerParentChildModel { Child = parent2Model, Parent = grandParent4Model });
            await _context.SaveChangesAsync();

            #endregion

            _address = "api/census/search/genealogy";

            // act
            var response = await _client.GetAsync($"{_address}?id=1&parentLevel=2");

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<AnswerViewModel>>>(await response.Content.ReadAsStringAsync());

            var resultList = result.ToList();
            Assert.Equal(3, resultList.Count);
            Assert.Single(resultList[0].ToList());
            Assert.Equal(2, resultList[1].ToList().Count);
            Assert.Equal(4, resultList[2].ToList().Count);
        }

        [Theory]
        [InlineData(null, null, 1, null, null, null, 5, 15)] //everyone at region 1
        [InlineData("Jo", NameComparisonEnum.StartWith, 2, null, null, null, 2, 15)] // every name/lastname that starts with 'jo' at region 2
        [InlineData("i", NameComparisonEnum.EndsWith, null, null, null, null, 4, 15)] // every name/lastname that ends with 'i'
        [InlineData(null, null, null, null, null, null, 15, 15)] // everyone
        [InlineData(null, null, 1, 1, 1, 1, 1, 15)] // everyone with region 1, gender 1, ethnicity 1 and schooling 1
        public async Task Filter(string name, NameComparisonEnum? nameComparison, int? region, int? gender, int? ethnicity, int? schooling, int expectedFraction, int expectedTotal)
        {
            // arrange
            _address = "api/census/search/filter";
            await SeedFilter();

            // act
            HttpResponseMessage response;
            if (string.IsNullOrWhiteSpace(name))
            {
                response = await _client.GetAsync($"{_address}?region={region}&gender={gender}&ethnicity={ethnicity}&schooling={schooling}");
            }
            else
            {
                response = await _client.GetAsync($"{_address}?name={name}&nameComparison={nameComparison}&region={region}&gender={gender}&ethnicity={ethnicity}&schooling={schooling}");
            }

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<SearchResultViewModel>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedFraction, result.Fraction);
            Assert.Equal(expectedTotal, result.Total);
        }


        private async Task SeedFilter()
        {
            _context.Answer.Add(new AnswerModel {FirstName = "João", LastName = "Silva", RegionId = 1, EthnicityId = 1, GenderId = 1, SchoolingId = 1});
            _context.Answer.Add(new AnswerModel {FirstName = "José", LastName = "Leite", RegionId = 2, EthnicityId = 2, GenderId = 2, SchoolingId = 2});
            _context.Answer.Add(new AnswerModel {FirstName = "Mara", LastName = "Sousa", RegionId = 3, EthnicityId = 1, GenderId = 1, SchoolingId = 3});
            _context.Answer.Add(new AnswerModel {FirstName = "João", LastName = "Lopez", RegionId = 1, EthnicityId = 2, GenderId = 2, SchoolingId = 4});
            _context.Answer.Add(new AnswerModel {FirstName = "Yara", LastName = "Sousa", RegionId = 2, EthnicityId = 1, GenderId = 1, SchoolingId = 5});
            _context.Answer.Add(new AnswerModel {FirstName = "Yuri", LastName = "Silva", RegionId = 3, EthnicityId = 2, GenderId = 2, SchoolingId = 1});
            _context.Answer.Add(new AnswerModel {FirstName = "João", LastName = "Leite", RegionId = 1, EthnicityId = 1, GenderId = 1, SchoolingId = 2});
            _context.Answer.Add(new AnswerModel {FirstName = "Ceci", LastName = "Silva", RegionId = 2, EthnicityId = 2, GenderId = 2, SchoolingId = 3});
            _context.Answer.Add(new AnswerModel {FirstName = "José", LastName = "Silva", RegionId = 3, EthnicityId = 1, GenderId = 1, SchoolingId = 4});
            _context.Answer.Add(new AnswerModel {FirstName = "Ceci", LastName = "Sousa", RegionId = 1, EthnicityId = 2, GenderId = 2, SchoolingId = 5});
            _context.Answer.Add(new AnswerModel {FirstName = "João", LastName = "Sousa", RegionId = 2, EthnicityId = 1, GenderId = 1, SchoolingId = 1});
            _context.Answer.Add(new AnswerModel {FirstName = "João", LastName = "Leite", RegionId = 3, EthnicityId = 2, GenderId = 2, SchoolingId = 2});
            _context.Answer.Add(new AnswerModel {FirstName = "Mara", LastName = "Sousa", RegionId = 1, EthnicityId = 1, GenderId = 1, SchoolingId = 3});
            _context.Answer.Add(new AnswerModel {FirstName = "Yuri", LastName = "Lopez", RegionId = 2, EthnicityId = 2, GenderId = 2, SchoolingId = 4});
            _context.Answer.Add(new AnswerModel {FirstName = "Yara", LastName = "Lopez", RegionId = 3, EthnicityId = 1, GenderId = 1, SchoolingId = 5});

            await _context.SaveChangesAsync();
        }
    }
}
