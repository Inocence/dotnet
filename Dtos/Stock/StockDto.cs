using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock.Comment;

namespace api.Dtos.Stock
{
    public class StockDto
    {
         public int Id { get; set; }
        public string  Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDvi { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarktetCap { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}