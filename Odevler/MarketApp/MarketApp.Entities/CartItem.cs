using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarketApp.Entities
{
    public class CartItem: IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Product Product { get; set; }
        public int Amount { get; set; }
        public double PastPrice { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public IList<Order> Orders { get; set; }

    }
}
