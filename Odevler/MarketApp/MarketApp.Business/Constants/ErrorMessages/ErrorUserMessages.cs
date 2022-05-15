using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.ErrorMessages
{
    public class ErrorUserMessages
    {
        public string NotFoundWithGivenUserId { get; } = "Verilen userId ile veritabanında kullanıcı bulunamadı";
        public string NotFoundWithGivenUsername { get; } = "Verilen username ile veritabanında kullanıcı bulunamadı";
        public string NotFoundWithGivenEmail { get; } = "Verilen email ile veritabanında kullanıcı bulunamadı";
        public string NotRegisteredWithGivenEmail { get; } = "Verilen email ile hiçbir kullanıcı kayıtlı değil";
        public string EmailOrPassWrong { get;  } = "Email ya da şifre yanlış";
        public string UserAlreadyUsingThisEmail { get; } = "Bu emaile sahip başka bir kullanıcı var";
        public string UserAlreadyUsingThisUsername { get; } = "Bu kullanıcı adına sahip başka bir kullanıcı var";
        public string NoUser { get;  } = "Veritabanında hiç kullanıcı yok";
    }
}
