using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.ErrorMessages
{
    public class ErrorOrderMessages
    {
        public string NotProceed { get;  } = "Sipariş gerçekleştirilemedi";
        public string NotFoundWithGivenOrderId { get; } = "Verilen orderId ile veritanabında sipariş bulunamadı";
        public string NoOrder { get; } = "Veritabanında hiç sipariş yok";
        public string NotFoundWithGivenUserId { get; } = "Verilen userId ile sipariş bulunamadı (Kullanıcının hiç siparişi yok)";
        public string PaymentFail { get; } = "Ödeme başarısız oldu";

    }
}
