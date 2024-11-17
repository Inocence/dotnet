using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using dotnet.Extensions;
using dotnet.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IStockRepository _stockRepo;
        public PortfolioController(
            UserManager<AppUser> userManager
            , IPortfolioRepository portfolioRepo
            , IStockRepository stockRepo
        )
        {
            _userManager = userManager;
            _portfolioRepo = portfolioRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolio()
        {
            var username = User.GetUerName();
            var appUser = await _userManager.FindByNameAsync(username);
            var portfolio = await _portfolioRepo.GetPortfolioAsync(appUser);
            return Ok(portfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol) {
            var username = User.GetUerName();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetStockBySymbolAsync(symbol);
            if(stock == null) return StatusCode(500, "Symbol not found");

            var portfolio = await _portfolioRepo.GetPortfolioByIdAsync(appUser.Id, symbol);
            if(portfolio) {
                return StatusCode(500, "Symbol duplicated");
            }
            var result = await _portfolioRepo.CreatePorfolioAsync(new Portfolio{
                AppUserId = appUser.Id,
                StockId = stock.Id
            });
            if(!result) {
                return StatusCode(500, "create failed");
            } 
            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol) {
            var username = User.GetUerName();
            var appUser = await _userManager.FindByNameAsync(username);

            var portfolio = await _portfolioRepo.DeletePortfolio(appUser.Id, symbol);
            if(portfolio ==null)  return NotFound();
            return Ok();
        }
    }
}