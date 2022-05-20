using MarketApp.Business.Data;
using MarketApp.Dtos.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.API.Models
{
    public class OrderPaymentModel
    {
        [Required]
        public int AddressId { get; set; }
        [Required]
        public IList<AddCartItemRequest> CartItems { get; set; }
        [Required]
        public PaymentProcessingInfos PaymentProcessingInfo { get; set; }
    }
}
