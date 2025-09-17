using api.Models;

namespace api.interfaces
{
    public interface IPorfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}