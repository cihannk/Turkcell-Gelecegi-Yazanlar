﻿using Newtonsoft.Json;
using System.Net;

namespace MarketApp.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            var result = new
            {
                ErrorCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message,
                Succeed = false,
            };

            var jsonResult = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(jsonResult);
        }
    }
}
