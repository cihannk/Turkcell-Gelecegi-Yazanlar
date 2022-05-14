using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Request
{
    public class UpdateAddressRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
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
