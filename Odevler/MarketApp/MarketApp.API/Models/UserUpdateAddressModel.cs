using System.ComponentModel.DataAnnotations;

namespace MarketApp.API.Models
{
    public class UserUpdateAddressModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string AddressDetail { get; set; }
        [Required]
        public string DesiredName { get; set; }
    }
}
