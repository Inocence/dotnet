using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        // private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            // _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] StockQuery stockQuery) {
            var stocks = await _stockRepo.GetAllAsync(stockQuery);
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null) {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto createRequestDto) {
            if(ModelState.IsValid) {
                return BadRequest();
            }
            var stockModel = createRequestDto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestDto updateDto) {
            var stockModel = updateDto.ToStockFromUpdateDto();
            var stock = await _stockRepo.UpdateAsync(id, stockModel);
            if(stock == null) {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null) {
                return NotFound();
            }
            return NoContent();
        }
    }
}