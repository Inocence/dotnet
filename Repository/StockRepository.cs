using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository: IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync(StockQuery stockQuery) {
            var stock = _context.Stock.Include(s => s.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(stockQuery.Symbol)) {
                stock = stock.Where(x => x.Symbol.Contains(stockQuery.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(stockQuery.CompanyName)) {
                stock = stock.Where(x => x.CompanyName.Contains(stockQuery.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(stockQuery.SortBy)) {
                if(stockQuery.SortBy.Equals(nameof(stockQuery.Symbol), StringComparison.OrdinalIgnoreCase)){
                    stock = stockQuery.IsDecending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (stockQuery.PageNumber - 1) * stockQuery.PageSize;
            return await stock.Skip(skipNumber).Take(stockQuery.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id) {
            return await _context.Stock.Include(s => s.Comments).ThenInclude(c => c.AppUser).FirstOrDefaultAsync(s => s.Id == id);
        }
        
        public async Task<Stock> CreateAsync(Stock stockModel) {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, Stock stockModel) {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stock == null) {
                return null;
            }

            stock.Symbol = stockModel.Symbol;
            stock.CompanyName = stockModel.CompanyName;
            stock.Purchase = stockModel.Purchase;
            stock.LastDvi = stockModel.LastDvi;
            stock.Industry = stockModel.Industry;
            stock.MarktetCap = stockModel.MarktetCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock?> DeleteAsync(int id) {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stock == null) {
                return null;
            }
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetStockBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(x => x.Symbol.ToLower() == symbol.ToLower());
        }
    }
}