using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public int? BookId { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
