using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using System.Linq;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stock
                .ToList()
                .Select(s => StockMappers.ToStockDto(s));

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            var stockDto = StockMappers.ToStockDto(stock);
            return Ok(stockDto);
        }
    }
}
