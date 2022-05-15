using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.ErrorMessages
{
    public class ErrorAddressMessages
    {
        public string NotFoundWithGivenAddressId { get; } = "Verilen addressId ile veritabanında adres bulunamadı";
        public string NoAddress { get; } = "Veritabanında hiç adres yok";
        public string NotFoundWithGivenUserId { get; } = "Verilen userId ile veritabanında adres bulunamadı";
        public string NotFoundWithGivenUsername { get;} = "Verilen username ile veritabanında adres bulunamadı";
        public string SameAddressExistWithGivenHeader { get; } = "Aynı başlıkta adres zaten var";
        public string NotBelongToToken { get; } = "Adres tokendeki kullanıcıya ait değil";
    }
}
