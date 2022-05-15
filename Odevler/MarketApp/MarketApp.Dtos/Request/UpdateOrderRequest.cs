using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int AddressId { get; set; }
        public IList<AddCartItemRequest> CartItems { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
