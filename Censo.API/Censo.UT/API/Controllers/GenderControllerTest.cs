namespace Censo.UT.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Censo.API.Controllers;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GenderControllerTest
    {
        private readonly Mock<IGenderRepository> _repository;
        private readonly GenderController _controller;

        public GenderControllerTest()
        {
            _repository = new Mock<IGenderRepository>();
            _controller = new GenderController(_repository.Object);
        }

        [Fact]
        public async Task Get()
        {
            // arrange
            var model = new GenderModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(model));

            // act
            var result = await _controller.Get(1);

            // assert
            _repository.Verify(x => x.GetAsync(1), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Same(model, (result.Result as OkObjectResult).Value);
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
            var model = new GenderModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((GenderModel)null));

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
            var model = new List<GenderModel>() { new GenderModel(), new GenderModel() };
            _repository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<GenderModel>>(model));

            // act
            var result = await _controller.GetAll();

            // assert
            _repository.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Same(model, (result.Result as OkObjectResult).Value);
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
            var model = new GenderModel { Id = 1 };
            _repository.Setup(x => x.CreateAsync(It.IsAny<GenderModel>())).Returns(Task.FromResult(model));

            // act
            var result = await _controller.Post(model);

            // assert
            _repository.Verify(x => x.CreateAsync(model), Times.Once);
            Assert.Same(model, (result.Result as CreatedAtActionResult).Value);
        }

        [Fact]
        public async Task Post_InternalError()
        {
            // arrange
            _repository.Setup(x => x.CreateAsync(It.IsAny<GenderModel>())).Throws(new Exception("teste"));

            // act
            var result = await _controller.Post(new GenderModel());

            // assert
            Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result.Result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result.Result as ObjectResult).Value);
        }

        [Fact]
        public async Task Put()
        {
            // arrange
            var model = new GenderModel();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<GenderModel>())).Returns(Task.FromResult(model));

            // act
            var result = await _controller.Put(model);

            // assert
            _repository.Verify(x => x.UpdateAsync(model), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_InternalError()
        {
            // arrange
            _repository.Setup(x => x.UpdateAsync(It.IsAny<GenderModel>())).Throws(new Exception("teste"));

            // act
            var result = await _controller.Put(new GenderModel());

            // assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, (result as ObjectResult).StatusCode);
            Assert.Equal("teste", (result as ObjectResult).Value);
        }
    }
}
