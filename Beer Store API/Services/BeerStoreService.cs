using Northwind.Data;
using Northwind.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Beer_Store_API.Services;

public class BeerStoreService : IBeerStoreService
{
    private readonly NorthwindContext _context;

    public BeerStoreService(NorthwindContext context)
    {
        _context = context;
    }
    bool IBeerStoreService.BeerExists(long id)
    {
        return (_context.Beers?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    bool IBeerStoreService.BreweryExists(long id)
    {
        return (_context.Breweries?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    bool IBeerStoreService.BarExists(long id)
    {
        return (_context.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    bool IBeerStoreService.BarBeerStockExists(long id)
    {
        return (_context.BarBeerStocks?.Any(e => e.Id == id)).GetValueOrDefault();
    }


    #region "Beer"
    async Task IBeerStoreService.PostBeer(Beer beer)
    {
        try
        {
            _context.Beers.Add(beer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                {
                    throw;
                }
            }
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
    async Task IBeerStoreService.PutBeer(long id, Beer beer)
    {
        try
        {
            _context.Entry(beer).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
    async Task<IEnumerable<Beer>> IBeerStoreService.GetBeer(double gtAlcoholByVolume, double ltAlcoholByVolume)
    {
        var query = _context.Beers.AsQueryable();
        if (gtAlcoholByVolume > 0) query = query.Where(t => t.PercentageAlcoholByVolume >= gtAlcoholByVolume);
        if (ltAlcoholByVolume > 0) query = query.Where(p => p.PercentageAlcoholByVolume <= ltAlcoholByVolume);
        return await query.ToListAsync();
    }

    async Task<ActionResult<Beer>> IBeerStoreService.GetBeerbyId(long id)
    {
        try
        {
            var beer = await _context.Beers.FindAsync(id);
            return beer;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    #endregion


    #region "Brewery"

    async Task IBeerStoreService.PostBrewery(Brewery brewery)
    {
        try
        {
            _context.Breweries.Add(brewery);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                {
                    throw;
                }
            }
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
    async Task IBeerStoreService.PutBrewery(long id, Northwind.Models.Brewery brewery)
    {
        try
        {
            _context.Entry(brewery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task<IEnumerable<Brewery>> IBeerStoreService.GetBrewery()
    {
        try
        {
            var brewery = _context.Breweries.ToList();
            return brewery;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task<ActionResult<Brewery>> IBeerStoreService.GetBrewerybyId(long id)
    {
        try
        {
            var brewery = await _context.Breweries.FindAsync(id);
            return brewery;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    #endregion

    #region "Bar"
    async Task IBeerStoreService.PostBar(Bar bar)
    {
        try
        {
            _context.Bars.Add(bar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                {
                    throw;
                }
            }
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task IBeerStoreService.PutBar(long id, Bar bar)
    {
        try
        {
            _context.Entry(bar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
    async Task<IEnumerable<Bar>> IBeerStoreService.GetBars()
    {
        try
        {
            var bar = _context.Bars.ToList();
            return bar;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task<ActionResult<Bar>> IBeerStoreService.GetBarbyId(long id)
    {
        try
        {
            var bar = await _context.Bars.FindAsync(id);
            return bar;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    #endregion

    #region "BreweryBeer"
    async Task IBeerStoreService.PostBreweryBeer(Beer beer)
    {
        try
        {
            _context.Beers.Add(beer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                {
                    throw;
                }
            }
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
    async Task<IEnumerable<Brewery>> IBeerStoreService.GetBreweryBeerbyId(long breweryId)
    {
        try
        {
            var result = _context.Breweries
                                .Include(e => e.Beers)
                                .Where(p => p.Id == breweryId)
                                .ToList();
            return result;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task<IEnumerable<Brewery>> IBeerStoreService.Brewery()
    {
        try
        {
            var bar = await _context.Breweries
                    .Include(e => e.Beers)
                    .ToListAsync();
            return bar;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }


    #endregion

    #region "BeerStock"
    async Task IBeerStoreService.PostBarBeerStock(BarBeerStock barBeerStock)
    {
        try
        {
            _context.BarBeerStocks.Add(barBeerStock);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                {
                    throw;
                }
            }
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task<IEnumerable<Bar>> IBeerStoreService.BarBeerStockDetailById(long barId)
    {
        try
        {
            var bar = await _context.Bars
                    .Include(e => e.BarBeerStocks)
                    .Where(b => b.Id == barId)
                    .ToListAsync();
            return bar;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    async Task<IEnumerable<Bar>> IBeerStoreService.BarBeerStockDetail()
    {
        try
        {
            var bar = await _context.Bars
           .Include(e => e.BarBeerStocks)
           .ToListAsync();
            return bar;
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    #endregion
}






