using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class AddOrderRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AddressId { get; set; }
        public IList<AddCartItemRequest> CartItems { get; set; }
    }
}
