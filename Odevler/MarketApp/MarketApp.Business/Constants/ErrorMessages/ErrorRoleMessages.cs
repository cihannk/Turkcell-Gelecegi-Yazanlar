using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.ErrorMessages
{
    public class ErrorRoleMessages
    {
        public string AlreadyExistWithGivenRoleName { get; } = "Veritabanında verilen roleName zaten var";
        public string NotFoundWithGivenRoleId { get; } = "Verilen roleId ile veritabanında rol bulunamadı";
        public string NoRole { get; } = "Veritabanında hiç rol yok";
    }
}
