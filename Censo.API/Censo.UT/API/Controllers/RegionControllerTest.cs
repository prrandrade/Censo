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

    public class RegionControllerTest
    {

        private readonly Mock<IRegionRepository> _repository;
        private readonly RegionController _controller;

        public RegionControllerTest()
        {
            _repository = new Mock<IRegionRepository>();
            _controller = new RegionController(_repository.Object);
        }


        [Fact]
        public void Get()
        {
            // arrange
            var model = new RegionModel();
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
            var model = new RegionModel();
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((RegionModel)null));

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
            var model = new List<RegionModel>() { new RegionModel(), new RegionModel() };
            _repository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<RegionModel>>(model));

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
            var model = new RegionModel { Id = 1 };
            _repository.Setup(x => x.CreateAsync(It.IsAny<RegionModel>())).Returns(Task.FromResult(model));

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
            var model = new RegionModel();
            _repository.Setup(x => x.UpdateAsync(It.IsAny<RegionModel>())).Returns(Task.FromResult(model));

            // act
            var result = _controller.Put(model).Result;

            // assert
            _repository.Verify(x => x.UpdateAsync(model), Times.Once);
            Assert.IsType<OkResult>(result);
        }
    }
}
