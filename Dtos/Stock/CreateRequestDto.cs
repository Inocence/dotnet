using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string  Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "CompanyName cannot be over 20 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDvi { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarktetCap { get; set; }
    }
}