﻿namespace Censo.IntegrationTest
{
    using System;
    using API;
    using Domain.Model;
    using Infra.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("Endpoints")]
    public class SchoolingControlerTest
    {
        private readonly HttpClient _client;
        private readonly DatabaseContext _context;
        private readonly string _address;

        public SchoolingControlerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>());

            _client = server.CreateClient();
            _context = server.Host.Services.GetService(typeof(DatabaseContext)) as DatabaseContext;
            _context.Database.EnsureDeleted(); // nedded for 'zeroing' the inmemory database between tests
            _address = "/api/census/schooling";
        }

        [Fact]
        public async Task Get()
        {
            // arrange
            var model = new SchoolingModel {Id = 1, Value = "Teste"};
            _context.Schoolings.Add(model);
            await _context.SaveChangesAsync();

            // act
            var response = await _client.GetAsync($"{_address}/1");

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<SchoolingModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(model.Value, result.Value);
        }

        [Fact]
        public async Task GetAll()
        {
            // arrange
            var model = new List<SchoolingModel>
            {
                new SchoolingModel {Id = 1, Value = "Teste"},
                new SchoolingModel {Id = 2, Value = "Outro teste"},
                new SchoolingModel {Id = 3, Value = "Mais um teste"}
            };
            _context.Schoolings.AddRange(model);
            await _context.SaveChangesAsync();

            // act
            var response = await _client.GetAsync(_address);

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<List<SchoolingModel>>(await response.Content.ReadAsStringAsync()).OrderBy(x => x.Id).ToList();

            Assert.Equal(model.Count, result.Count);
            Assert.Equal(model[0].Id, result[0].Id);
            Assert.Equal(model[1].Id, result[1].Id);
            Assert.Equal(model[2].Id, result[2].Id);
        }

        [Fact]
        public async Task Post()
        {
            // arrange
            var model = new SchoolingModel { Value = "teste" };

            // act
            var response = await _client.PostAsync(_address, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            // assert
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<SchoolingModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(model.Value, result.Value);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Put()
        {
            // arrange
            var model = new SchoolingModel {Id = 4, Value = "Teste 4"};
            var newModel = new SchoolingModel {Id = 4, Value = "Novo Teste 4"};
            _context.Schoolings.Add(model);
            await _context.SaveChangesAsync();

            // act
            var response = await _client.PutAsync(_address, new StringContent(JsonConvert.SerializeObject(newModel), Encoding.UTF8, "application/json"));
            _context.Entry(model).Reload();

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(newModel.Value, model.Value);
        }
    }
}
