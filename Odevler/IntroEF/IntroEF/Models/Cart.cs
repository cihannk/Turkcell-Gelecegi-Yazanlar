using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
