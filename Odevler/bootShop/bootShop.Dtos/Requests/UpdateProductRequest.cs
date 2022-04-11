using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Dtos.Requests
{
    public class UpdateProductRequest
    {
        [Required]
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Ürün adı en az üç karakter olmalıdır")]
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public string ImageUrl { get; set; }
    }
}
