using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.SuccessMessages
{
    public class SuccessOrderMessages
    {
        public string SuccessfullyCreated { get; } = "Sipariş başarıyla oluşturuldu";
        public string SuccessfullyCreatedUserOrder { get; } = "Sipariş başarıyla verildi";
        public string SuccessfullyUpdated { get; } = "Sipariş başarıyla güncellendi";
        public string SuccessfullyDeleted { get; } = "Sipariş başarıyla silindi";
    }
}
