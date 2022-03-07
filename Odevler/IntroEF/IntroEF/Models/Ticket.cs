using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroEF.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public DateTime Expiration { get; set; }
        public double DiscountPercent { get; set; }
    }
}
