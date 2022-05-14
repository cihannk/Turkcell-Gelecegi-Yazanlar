using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class UpdateCategoryRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
