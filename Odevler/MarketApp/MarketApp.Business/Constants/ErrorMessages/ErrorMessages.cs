namespace MarketApp.Business.Constants.ErrorMessages
{
    public static class ErrorMessages
    {
        public static ErrorAddressMessages Address { get;} = new ErrorAddressMessages();
        public static ErrorUserMessages User { get;  } = new ErrorUserMessages();
        public static ErrorCategoryMessages Category { get; } = new ErrorCategoryMessages();
        public static ErrorOrderMessages Order { get;} = new ErrorOrderMessages();
        public static ErrorProductMessages Product { get;  } = new ErrorProductMessages();
        public static ErrorRoleMessages Role { get;  } = new ErrorRoleMessages();
    }
}
