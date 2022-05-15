using MarketApp.Business.Data;
using MarketApp.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.API.Models
{
    public class OrderPaymentModel
    {
        public int AddressId { get; set; }
        public IList<AddCartItemRequest> CartItems { get; set; }
        public PaymentProcessingInfos PaymentProcessingInfo { get; set; }
    }
}
