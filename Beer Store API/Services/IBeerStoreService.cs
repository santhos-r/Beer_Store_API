using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Beer_Store_API.Services
{
    public interface IBeerStoreService
    {
        #region "Beers"
        Task PostBeer(Beer beer);
        Task PutBeer(long id, Beer beer);
        Task<IEnumerable<Beer>> GetBeer([FromQuery] double gtAlcoholByVolume, [FromQuery] double ltAlcoholByVolume);
        Task<ActionResult<Beer>> GetBeerbyId(long id);
        bool BeerExists(long id);
        #endregion

        #region "Brewery"
        Task PostBrewery(Brewery brewery);
        Task PutBrewery(long id, Brewery brewery);
        Task<IEnumerable<Brewery>> GetBrewery();
        Task<ActionResult<Brewery>> GetBrewerybyId(long id);
        bool BreweryExists(long id);
        #endregion

        #region "Bars"
        Task PostBar(Bar bar);
        Task PutBar(long id, Bar bar);
        Task<IEnumerable<Bar>> GetBars();
        Task<ActionResult<Bar>> GetBarbyId(long id);
        bool BarExists(long id);
        #endregion

        #region "BreweryBeers"
        Task PostBreweryBeer(Beer beer);
        Task<IEnumerable<Brewery>> GetBreweryBeerbyId(long breweryId);
        Task<IEnumerable<Brewery>> Brewery();

        #endregion

        #region "BeerStock"
        Task PostBarBeerStock(BarBeerStock barBeerStock);
        Task<IEnumerable<Bar>> BarBeerStockDetailById(long barId);
        Task<IEnumerable<Bar>> BarBeerStockDetail();
        bool BarBeerStockExists(long id);
        #endregion
    }
}

