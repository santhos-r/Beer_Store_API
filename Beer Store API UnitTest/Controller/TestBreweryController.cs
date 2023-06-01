using Moq;
using Xunit;
using Beer_Store_API.Controllers;
using Beer_Store_API.Services;
using Beer_Store_API_UnitTest.MockData;
using FluentAssertions;
using AutoFixture;
using Northwind.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Beer_Store_API_UnitTest.Controller
{
    public  class TestBreweryController
    {
        private readonly IFixture _fixture;
        private readonly Mock<IBeerStoreService> _serviceMock;
        private readonly BreweryController _sut;
        private readonly Mock<IBeerStoreService> beerStoreService;
        public TestBreweryController()
        {
            _fixture = new Fixture();
            //_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            //_fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            _serviceMock = _fixture.Freeze<Mock<IBeerStoreService>>();
            _sut = new BreweryController(_serviceMock.Object);
            beerStoreService = new Mock<IBeerStoreService>();
        }

        [Fact]
        public async Task GetBrewery_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
  .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var BreweryMock = _fixture.Create<IEnumerable<Brewery>>();
            _serviceMock.Setup(x=>x.GetBrewery()).ReturnsAsync(BreweryMock);
            //Act
            var result = await _sut.GetBrewery().ConfigureAwait(false);
            //Assert
            result.Should().NotBeNull();
            // result.Should().BeAssignableTo<IActionResult>();
            //result.Should().BeAssignableTo<OkObjectResult>();
            //result.As<OkObjectResult>().Value
            //    .Should()
            //    .NotBeNull()
            //    .And.BeOfType(BreweryMock.GetType());

            //_serviceMock.Verify(x => x.GetBrewery(), Times.Once());

        }

        [Fact]
        public async Task GetBrewery_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            List<Brewery> response = null;
            _serviceMock.Setup(x => x.GetBrewery()).ReturnsAsync(response);          
            
            //Act
            var result = await _sut.GetBrewery().ConfigureAwait(false);
            //Assert
            //result.Should().BeNull();
             result.Should().BeAssignableTo<NotFoundResult>();
            //result.Should().BeAssignableTo<OkObjectResult>();
            //result.As<OkObjectResult>().Value
            //    .Should()
            //    .NotBeNull()
            //    .And.BeOfType(BreweryMock.GetType());

           // _serviceMock.Verify(x => x.GetBrewery(), Times.Once());

        }

        [Theory]
        [InlineData(2)]
        public async Task GetBreweryById_ShouldReturnOkResponse_WhenValidInput(int Id)
        {
            //Arrange
            
            var BreweryList = BeerMockData.GetBreweries();
            beerStoreService.Setup(_ => _.GetBrewerybyId(Id)).ReturnsAsync(BreweryList[1]);
            var sut = new BreweryController(beerStoreService.Object);
            //systemundertest
            //Act          
            var result = await sut.GetBrewerybyId(Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(BreweryList[1].Id, result.Value.Id);
            Assert.True(BreweryList[1].Id == result.Value.Id);

        }

    }
}
