using System;
using System.ComponentModel.DataAnnotations;

namespace bootShop.Entities
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün adı boş olamaz")]
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ImageUrl { get; set; }

    }
}
