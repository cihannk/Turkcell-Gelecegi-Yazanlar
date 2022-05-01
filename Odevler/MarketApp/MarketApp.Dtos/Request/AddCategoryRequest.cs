using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class AddCategoryRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
