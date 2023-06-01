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
    [Route("api/bar")]
    [ApiController]
    public class BarsController : ControllerBase
    {
        private readonly IBeerStoreService _storeService;

        public BarsController(IBeerStoreService storeService)
        {
            _storeService = storeService;
        }
        // POST /bar
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Bar>> PostBar(Bar bar)
        {
            await _storeService.PostBar(bar);
            return CreatedAtAction("PostBar", new { id = bar.Id }, bar);
        }

        //// PUT /bar/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutBar(long id, Bar bar)
        {
            try
            {
                if (id != bar.Id)
                {
                    return BadRequest();
                }
                await _storeService.PutBar(id, bar);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarExists(id))
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

        // GET /bar
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Bar>> GetBars()
        {

            try
            {
                var result = await _storeService.GetBars();
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }            
        }

        // GET /bar/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Bar>> GetBarbyId(long id)
        {
            try
            {
                var result = await _storeService.GetBarbyId(id);
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

        private bool BarExists(long id)
        {
            var result = _storeService.BarExists(id);
            return result;
        }
    }
}
