using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.ErrorMessages
{
    public class ErrorProductMessages
    {
        public string AlreadyExistWithGivenProductName { get;  } = "Aynı adlı ürün zaten var";
        public string NotFoundWithGivenProductId { get;  } = "Verilen productId ile veritabanında ürün bulunamadı";
        public string NoProduct { get;  } = "Veritabanında hiç ürün yok";
        public string NotFoundWithGivenCategoryName { get; } = "Verilen categoryName ile veritabanında ürünler bulunamadı";
    }
}
