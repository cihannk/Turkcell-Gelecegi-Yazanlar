using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class AddAddressRequest
    {
        [Required]
        public int UserId { get; set; }
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
