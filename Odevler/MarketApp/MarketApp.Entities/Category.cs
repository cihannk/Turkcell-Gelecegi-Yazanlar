using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MarketApp.Entities
{
    public class Category: IEntity
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