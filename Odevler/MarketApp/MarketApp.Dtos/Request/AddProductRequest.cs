using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class AddProductRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public double? Discount { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
