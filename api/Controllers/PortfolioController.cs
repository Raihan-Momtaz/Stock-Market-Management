using api.Dtos.Account;
using api.Extensions;
using api.interfaces;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;

        private readonly IPorfolioRepository _portfolioRepo;

        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepo,
            IPorfolioRepository porfolioRepo
        )
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = porfolioRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.GetUsername();
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("Username claim not found.");
            }

            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                return NotFound("User not found.");
            }


            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

    }
}
