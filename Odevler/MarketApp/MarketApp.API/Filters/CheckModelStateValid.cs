using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketApp.API.Filters
{
    public class CheckModelStateValid : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
