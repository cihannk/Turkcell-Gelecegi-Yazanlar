using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class AddCartItemRequest
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double PastPrice { get; set; }
    }
}
