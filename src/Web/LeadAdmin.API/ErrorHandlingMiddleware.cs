using Newtonsoft.Json;
using System.Net;

namespace LeadAdmin.API
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
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // Specify different custom exceptions here
            //if (ex is CustomException) code = HttpStatusCode.BadRequest;

            var message = ex.Message;
            if (message.Contains("connection", StringComparison.OrdinalIgnoreCase))
            {
                message = "Invalid connection";
            }

            if (message.Contains("Initial Catalog", StringComparison.OrdinalIgnoreCase))
            {
                message = "Invalid connection";
            }

            if (message.Contains("Password", StringComparison.OrdinalIgnoreCase))
            {
                message = "Invalid connection";
            }

            string result = JsonConvert.SerializeObject(new { Message = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
