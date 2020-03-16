namespace Censo.UnitTest.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Censo.API.Controllers;
    using Censo.API.Hubs;
    using Censo.API.ViewModels;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.AspNetCore.Components.RenderTree;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AnswerControllerTest
    {
        private readonly Mock<IAnswerRepository> _repository;
        private readonly Mock<DashboardHub> _hub;
        private readonly AnswerController _controller;

        public AnswerControllerTest()
        {
            _repository = new Mock<IAnswerRepository>();
            _hub = new Mock<DashboardHub>();
            _controller = new AnswerController(_repository.Object, _hub.Object);
        }

        [Fact]
        public async Task Get()
        {
            // arrange
            var model = new AnswerModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(model));

            // act
            var result = await _controller.Get(1);

            // assert
            _repository.Verify(x => x.GetAsync(1), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<AnswerViewModel>((result.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task Get_InternalError()
        {
            // arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws(new Exception("teste"));

            // act
            var result = await _controller.Get(1);

            // assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result.Result as ObjectResult).Value);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            // arrange
            var model = new AnswerModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((AnswerModel)null));

            // act
            var result = await _controller.Get(1);

            // assert
            _repository.Verify(x => x.GetAsync(1), Times.Once);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAll()
        {
            // arrange
            var model = new List<AnswerModel>() { new AnswerModel(), new AnswerModel() };
            _repository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<AnswerModel>>(model));

            // act
            var result = await _controller.GetAll();

            // assert
            _repository.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<AnswerViewModel>>((result.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task GetAll_InternalError()
        {
            // arrange
            _repository.Setup(x => x.GetAllAsync()).Throws(new Exception("teste"));

            // act
            var result = await _controller.GetAll();

            // assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result.Result as ObjectResult).Value);
        }

        [Fact]
        public async Task Post()
        {
            // arrange
            var vm = new AnswerViewModel { Info = new AnswerInfoViewModel()};
            _repository.Setup(x => x.CreateWithParentsAndChidrenAsync(It.IsAny<AnswerModel>(), It.IsAny<IEnumerable<AnswerModel>>(), It.IsAny<IEnumerable<AnswerModel>>())).Returns(Task.FromResult(new AnswerModel()));

            // act
            var result = await _controller.Post(vm);

            // assert
            _repository.Verify(x => x.CreateWithParentsAndChidrenAsync(It.IsAny<AnswerModel>(), null, null), Times.Once);
            Assert.IsType<AnswerViewModel>((result.Result as CreatedAtActionResult).Value);
        }

        [Fact]
        public async Task Post_InternalError()
        {
            // arrange
            _repository.Setup(x => x.CreateWithParentsAndChidrenAsync(It.IsAny<AnswerModel>(), It.IsAny<IEnumerable<AnswerModel>>(), It.IsAny<IEnumerable<AnswerModel>>())).Throws(new Exception("teste"));

            // act
            var result = await _controller.Post(new AnswerViewModel{Info = new AnswerInfoViewModel()});

            // assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result.Result as ObjectResult).Value);
        }
    }
}
