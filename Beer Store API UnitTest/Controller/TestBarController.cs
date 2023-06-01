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
    public  class TestBarController
    {
        private readonly IFixture _fixture;
        private readonly Mock<IBeerStoreService> _serviceMock;
        private readonly BarsController _sut;
        public TestBarController()
        {
            _fixture = new Fixture();
            //_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            //_fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            _serviceMock = _fixture.Freeze<Mock<IBeerStoreService>>();
            _sut = new BarsController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetBars_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
.ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var BarMock = _fixture.Create<IEnumerable<Bar>>();
            _serviceMock.Setup(x=>x.GetBars()).ReturnsAsync(BarMock);
            //Act
            var result = await _sut.GetBars().ConfigureAwait(false);
            //Assert
            result.Should().NotBeNull();
            //result.Should().BeAssignableTo<ActionResult<IEnumerable<Bar>>>();
            //result.Should().BeAssignableTo<OkObjectResult>();
            //result.As<OkObjectResult>().Value
            //    .Should()
            //    .NotBeNull()
            //    .And.BeOfType(BarMock.GetType());

            //_serviceMock.Verify(x=>x.GetBars(), Times.Once());

        }

    }
}
