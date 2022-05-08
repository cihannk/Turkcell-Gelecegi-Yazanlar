using MarketApp.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IPaymentService
    {
        Task<bool> Processing(PaymentProcessingInfos paymentInfo);
    }
}
