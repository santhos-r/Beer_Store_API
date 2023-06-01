using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beer_Store_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Beer_Store_API.Controllers
{
    //[Route("api/bar")]
    [ApiController]
    public class BarBeerStocksController : ControllerBase
    {
        private readonly IBeerStoreService _storeService;

        public BarBeerStocksController(IBeerStoreService storeService)
        {
            _storeService = storeService;
        }

        // POST /bar/beer
        [HttpPost("api/bar/beer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BarBeerStock>> PostBarBeerStock(BarBeerStock barBeerStock)
        {
            await _storeService.PostBarBeerStock(barBeerStock);
            return CreatedAtAction("PostBarBeerStock", new { id = barBeerStock.Id }, barBeerStock);            
        }

        // GET /bar/{barId}/beer
        [HttpGet("api/bar/{barId:int}/beer", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Bar>> BarBeerStockDetailbyId(long barId)
        {
            try
            {
                var result = await _storeService.BarBeerStockDetailById(barId);                
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        // GET /bar/beer 
        [HttpGet("api/bar/beer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Bar>> BarBeerStockDetail()
        {
            try
            {
                var result = await _storeService.BarBeerStockDetail();
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
        private bool BarBeerStockExists(long id)
        {
            var result = _storeService.BarBeerStockExists(id);
            return result;
        }
    }
}
