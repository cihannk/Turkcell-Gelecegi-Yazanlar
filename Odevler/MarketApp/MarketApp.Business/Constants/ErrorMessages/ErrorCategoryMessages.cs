using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.ErrorMessages
{
    public class ErrorCategoryMessages
    {
        public string AlreadyExistWithGivenName { get; } = "Aynı isimde bir kategori zaten var";
        public string NoCategory { get; } = "Veritabanında hiç kategori yok";
        public string NotFoundWithGivenCategoryId { get; } = "Verilen kategori id ile veritanabında kategori bulunamadı";
        public string NotFoundWithGivenCategoryName { get; } = "Verilen categoryName ile veritanabında kategori bulunamadı";

    }
}
