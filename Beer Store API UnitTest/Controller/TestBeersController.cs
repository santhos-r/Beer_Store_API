using Moq;
using Xunit;
using Beer_Store_API.Controllers;
using Beer_Store_API.Services;
using Beer_Store_API_UnitTest.MockData;
using FluentAssertions;
using AutoFixture;

namespace Beer_Store_API_UnitTest.Controller
{
    public class TestBeersController
    {
private readonly Mock<IBeerStoreService> beerStoreService;
         public TestBeersController()
        {
            beerStoreService = new Mock<IBeerStoreService>();
        }
        [Fact]
        public async Task GetBeerbyId_Status201Created()
        {

            //Arrange
            long Id = 5;   
            var beerList = BeerMockData.GetBeer();        
            beerStoreService.Setup(_ => _.GetBeerbyId(Id)).ReturnsAsync(beerList[0]);
            var sut = new BeerController(beerStoreService.Object);
            //systemundertest
            //Act          
                var result =  await sut.GetBeerbyId(Id);

            //Assert
             Assert.NotNull(result);
            Assert.Equal(beerList[0].Id, result.Value.Id);
            Assert.True(beerList[0].Id == result.Value.Id);

        }

    
    }
}
;