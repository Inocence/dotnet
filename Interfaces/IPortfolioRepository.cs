using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace dotnet.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetPortfolioAsync(AppUser appUser);
        Task<bool> CreatePorfolioAsync(Portfolio portfolio);
        Task<bool> GetPortfolioByIdAsync(string appUserId, string symbol);
        Task<Portfolio?> DeletePortfolio(string appUserId, string symbol);
    }
}