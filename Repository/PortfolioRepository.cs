using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using dotnet.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePorfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolio.AddAsync(portfolio);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<Portfolio?> DeletePortfolio(string appUserId, string symbol)
        {
            var portfolio = await _context.Portfolio.FirstOrDefaultAsync(x => x.AppUserId == appUserId
             && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if(portfolio == null) return null;

            _context.Portfolio.Remove(portfolio); 
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetPortfolioAsync(AppUser appUser)
        {
            var port = await _context.Portfolio.Where(x => x.AppUserId == appUser.Id)
            .Select(p => new Stock {
                Id = p.StockId,
                Symbol = p.Stock.Symbol,
                CompanyName = p.Stock.CompanyName,
                Purchase = p.Stock.Purchase,
                LastDvi = p.Stock.LastDvi,
                Industry = p.Stock.Industry,
                MarktetCap = p.Stock.MarktetCap,
            })
            .ToListAsync();
            return port;
        }

        public async Task<bool> GetPortfolioByIdAsync(string appUserId, string symbol)
        {
            var result = await _context.Portfolio.AnyAsync(
                x => x.AppUserId == appUserId 
                && x.Stock.Symbol.ToLower() == symbol.ToLower());  
            return result;
        }
    }
}