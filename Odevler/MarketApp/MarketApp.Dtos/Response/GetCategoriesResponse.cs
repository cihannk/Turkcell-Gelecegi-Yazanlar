using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Response
{
    public class GetCategoriesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        [IgnoreDataMember]
        public IList<Product> Products { get; set; }
    }
}
