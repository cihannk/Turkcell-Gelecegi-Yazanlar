using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Response
{
    public class GetProductsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public double Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
