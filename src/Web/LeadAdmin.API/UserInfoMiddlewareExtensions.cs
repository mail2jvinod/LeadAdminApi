using Microsoft.AspNetCore.Builder;

namespace LeadAdmin.API
{
    public static class UserInfoMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserInfo(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserInfoMiddleware>();
        }
    }
}
