using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beer_Store_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Beer_Store_API.Controllers
{
    [Route("api/brewery")]
    [ApiController]
    public class BreweryBeerController : ControllerBase
    {
        private readonly IBeerStoreService _storeService;

        public BreweryBeerController(IBeerStoreService storeService)
        {
            _storeService = storeService;
        }

        // POST: api/BreweryBeer
        [HttpPost("beer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            await _storeService.PostBreweryBeer(beer);
            return CreatedAtAction("PostBeer", new { id = beer.Id }, beer);           
        }

        // GET: /brewery/{breweryId}/beer
        [HttpGet("{breweryId}/beer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Brewery>> GetBreweryBeerbyId(long breweryId)
        {
            try
            {
                var result = await _storeService.GetBreweryBeerbyId(breweryId);             
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        // GET /brewery/beer
        [HttpGet("beer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Brewery>> Brewery()
        {
            try
            {
                var result = await _storeService.Brewery();
                return result;                
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }       
    }
}
