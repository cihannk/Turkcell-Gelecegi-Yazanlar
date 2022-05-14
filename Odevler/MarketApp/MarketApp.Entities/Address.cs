using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarketApp.Entities
{
    public class Address: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public User User { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string AddressDetail { get; set; }
        public string DesiredName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public IList<Order> Orders { get; set; }    
    }
}
