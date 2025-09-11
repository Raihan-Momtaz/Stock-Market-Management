using api.Dtos.Stock;
using api.Models; // make sure you import your Stock model

namespace api.Mappers
{
    public class StockMappers
    {
        public static StockDto ToStockDto(Stock stockModel) 
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                LastDiv = stockModel.LastDiv
            };
        }
    }
}
