using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using System.Linq;
using api.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using api.interfaces;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        // GET: api/stock
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => StockMappers.ToStockDto(s));
            return Ok(stockDto);
        }

        // GET: api/stock/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            var stockDto = StockMappers.ToStockDto(stock);
            return Ok(stockDto);
        }

        // POST: api/stock
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stockModel);
            

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, StockMappers.ToStockDto(stockModel));
        }

        // PUT: api/stock/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id,updateDto);

            if (stockModel == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();

            return Ok(StockMappers.ToStockDto(stockModel));
        }

        // DELETE: api/stock/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
