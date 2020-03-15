namespace Censo.UnitTest.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Censo.API.Controllers;
    using Censo.API.ViewModels;
    using Domain;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class SearchControllerTest
    {
        private readonly Mock<IAnswerRepository> _repository;
        private readonly SearchController _controller;

        public SearchControllerTest()
        {
            _repository = new Mock<IAnswerRepository>();
            _controller = new SearchController(_repository.Object);
        }

        [Fact]
        public async Task Filter()
        {
            // arrange
            var (searchResult, total) = new Tuple<int, int>(1, 2);
            _repository.Setup(x => x.ApplyFilterAsync(It.IsAny<string>(), It.IsAny<NameComparisonEnum>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult((searchResult, total)));

            // act
            var result = await _controller.Filter("teste", NameComparisonEnum.Contains, 1, 2, 3, 4);

            // assert
            _repository.Verify(x => x.ApplyFilterAsync("teste", NameComparisonEnum.Contains, 1, 2, 3, 4), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(searchResult, ((result.Result as OkObjectResult).Value as SearchResultViewModel).Fraction);
            Assert.Equal(total, ((result.Result as OkObjectResult).Value as SearchResultViewModel).Total);
        }

        [Fact]
        public async Task Filter_InternalError()
        {
            // arrange
            _repository.Setup(x => x.ApplyFilterAsync(It.IsAny<string>(), It.IsAny<NameComparisonEnum>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws(new Exception("teste"));

            // act
            var result = await _controller.Filter("", NameComparisonEnum.Contains);

            // assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result.Result as ObjectResult).Value);
        }

        [Fact]
        public async Task Genealogy()
        {
            // arrange
            var model = new List<List<AnswerModel>>
            {
                new List<AnswerModel> {new AnswerModel { FirstName = "1", LastName = "2"}},
                new List<AnswerModel> {new AnswerModel { FirstName = "2", LastName = "4"}, new AnswerModel{ FirstName = "5", LastName = "6"}}
            };
            _repository.Setup(x => x.ApplyGenealogyFilter(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(model));

            // act
            var result = await _controller.Genealogy(1, 1);

            // assert
            _repository.Verify(x => x.ApplyGenealogyFilter(1, 1), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<IEnumerable<AnswerViewModel>>>((result.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task Genealogy_InternalError()
        {
            // arrange
            _repository.Setup(x => x.ApplyGenealogyFilter(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("teste"));

            // act
            var result = await _controller.Genealogy(1, 1);

            // assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result.Result as ObjectResult).Value);
        }
    }
}
