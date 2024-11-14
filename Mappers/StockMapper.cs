using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Dtos.Stock.Comment;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel) {
            return new StockDto{
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDvi = stockModel.LastDvi,
                Industry = stockModel.Industry,
                MarktetCap = stockModel.MarktetCap,
                Comments = stockModel.Comments.Select(c => c.RespCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateRequestDto createRequestDto) {
            return new Stock{
                Symbol = createRequestDto.Symbol,
                CompanyName = createRequestDto.CompanyName,
                Purchase = createRequestDto.Purchase,
                LastDvi = createRequestDto.LastDvi,
                Industry = createRequestDto.Industry,
                MarktetCap = createRequestDto.MarktetCap
            };
        }

        public static Stock ToStockFromUpdateDto(this UpdateRequestDto updateRequestDto) {
            return new Stock{
                Symbol = updateRequestDto.Symbol,
                CompanyName = updateRequestDto.CompanyName,
                Purchase = updateRequestDto.Purchase,
                LastDvi = updateRequestDto.LastDvi,
                Industry = updateRequestDto.Industry,
                MarktetCap = updateRequestDto.MarktetCap
            };
        }
    }
}