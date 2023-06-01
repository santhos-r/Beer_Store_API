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
    public class BreweryController : ControllerBase
    {
        private readonly IBeerStoreService _storeService;
        public BreweryController(IBeerStoreService storeService)
        {
            _storeService = storeService;
        }

        // POST: api/brewery
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Brewery>> PostBrewery(Brewery brewery)
        {
            await _storeService.PostBrewery(brewery);
            return CreatedAtAction("PostBrewery", new { id = brewery.Id }, brewery);
        }

        // PUT: api/Breweries/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutBrewery(long id, Brewery brewery)
        {

            try
            {
                if (id != brewery.Id)
                {
                    return BadRequest();
                }
                await _storeService.PutBrewery(id, brewery);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
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

        // GET: GET /brewery
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Brewery>> GetBrewery()
        {
            try
            {
                var result = await _storeService.GetBrewery();
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        // GET /brewery/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Brewery>> GetBrewerybyId(long id)
        {
            try
            {
                var result = await _storeService.GetBrewerybyId(id);
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

        private bool BreweryExists(long id)
        {
            var result = _storeService.BreweryExists(id);
            return result;
        }



    }
}
