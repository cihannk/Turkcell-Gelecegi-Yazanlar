using MarketApp.Business.Abstract;
using MarketApp.Business.Data;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Concrete
{
    public class StripePaymentService: IPaymentService
    {
        public async Task<bool> Processing(PaymentProcessingInfos paymentInfo)
        {
            var optionsCharge = new ChargeCreateOptions
            {
                Amount = paymentInfo.Amount,
                Currency = paymentInfo.Currency,
                Description = paymentInfo.Description,
                Source = paymentInfo.Token,
                ReceiptEmail = paymentInfo.CustomerEmail
            };
            var serviceCharge = new ChargeService();
            Charge charge = await serviceCharge.CreateAsync(optionsCharge);
            if (charge.Status == "succeeded")
            {
                return true;
            }
            return false;
        }
    }
}
