namespace Censo.UT.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Censo.API.Controllers;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class EthnicityControllerTest
    {

        private readonly Mock<IEthnicityRepository> _repository;
        private readonly EthnicityController _controller;

        public EthnicityControllerTest()
        {
            _repository = new Mock<IEthnicityRepository>();
            _controller = new EthnicityController(_repository.Object);
        }


        [Fact]
        public void Get()
        {
            // arrange
            var model = new EthnicityModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(model));

            // act
            var result = _controller.Get(1).Result;

            // assert
            _repository.Verify(x => x.GetAsync(1), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Same(model, (result.Result as OkObjectResult).Value);
        }

        [Fact]
        public void Get_NotFound()
        {
            // arrange
            var model = new EthnicityModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((EthnicityModel)null));

            // act
            var result = _controller.Get(1).Result;

            // assert
            _repository.Verify(x => x.GetAsync(1), Times.Once);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAll()
        {
            // arrange
            var model = new List<EthnicityModel>() { new EthnicityModel(), new EthnicityModel() };
            _repository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<EthnicityModel>>(model));

            // act
            var result = _controller.GetAll().Result;

            // assert
            _repository.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.Same(model, result);
        }

        [Fact]
        public void Post()
        {
            // arrange
            var model = new EthnicityModel { Id = 1 };
            _repository.Setup(x => x.CreateAsync(It.IsAny<EthnicityModel>())).Returns(Task.FromResult(model));

            // act
            var result = _controller.Post(model).Result;

            // assert
            _repository.Verify(x => x.CreateAsync(model), Times.Once);
            Assert.Same(model, (result.Result as CreatedAtActionResult).Value);
        }

        [Fact]
        public void Put()
        {
            // arrange
            var model = new EthnicityModel();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<EthnicityModel>())).Returns(Task.FromResult(model));

            // act
            var result = _controller.Put(model).Result;

            // assert
            _repository.Verify(x => x.UpdateAsync(model), Times.Once);
            Assert.IsType<OkResult>(result);
        }
    }
}
