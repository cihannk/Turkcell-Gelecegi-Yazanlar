using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarketApp.Entities
{
    public class Order: IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User User { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public IList<CartItem> CartItems { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
