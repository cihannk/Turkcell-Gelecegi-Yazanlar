using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public double Total { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
