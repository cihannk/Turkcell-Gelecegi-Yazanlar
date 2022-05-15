using System.ComponentModel.DataAnnotations;

namespace MarketApp.API.Models
{
    public class UserAddAddressModel
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Street { get; set; }
        public string AddressDetail { get; set; }
        [Required]
        [MinLength(3)]
        public string DesiredName { get; set; }
    }
}
