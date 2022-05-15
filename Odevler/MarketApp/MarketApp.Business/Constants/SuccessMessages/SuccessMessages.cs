using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Constants.SuccessMessages
{
    public class SuccessMessages
    {
        public static SuccessAddressMessages Address { get; } = new SuccessAddressMessages();
        public static SuccessCategoryMessages Category { get; } = new SuccessCategoryMessages();
        public static SuccessOrderMessages Order { get; } = new SuccessOrderMessages(); 
        public static SuccessProductMessages Product { get; } = new SuccessProductMessages();
        public static SuccessRoleMessages Role { get; } = new SuccessRoleMessages();
        public static SuccessUserMessages User { get; } = new SuccessUserMessages();
    }
}
