using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Beer_Store_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Beer_Store_API.Controllers
{
    [Route("api/beer")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerStoreService _storeService;

        public BeerController(IBeerStoreService storeService)
        {
            _storeService = storeService;
        }

        //POST /beer - Insert a single beer
       [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            await _storeService.PostBeer(beer);
            return CreatedAtAction("PostBeer", new { id = beer.Id }, beer);
        }

        //- PUT /beer/{id}        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutBeer(long id, Beer beer)
        {
            try
            {
                if (id != beer.Id)
                {
                    return BadRequest();
                }
               await _storeService.PutBeer(id, beer);
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        // GET /beer?gtAlcoholByVolume=5.0&ltAlcoholByVolume=8.0 - Get all beers with optional filtering query parameters for alcohol content (gtAlcoholByVolume = greater than, ltAlcoholByVolume = less than)
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Beer>> GetBeer([FromQuery] double gtAlcoholByVolume, [FromQuery] double ltAlcoholByVolume)
        {
            try
            {
                var result = await _storeService.GetBeer(gtAlcoholByVolume, ltAlcoholByVolume);
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        // GET /beer/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        public async Task<ActionResult<Beer>> GetBeerbyId(long id)
        {
            try
            {
                var result = await _storeService.GetBeerbyId(id);
                if (result.Value == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        private bool BeerExists(long id)
        {
            var result = _storeService.BeerExists(id);
            return result;            
        }
    }
}
