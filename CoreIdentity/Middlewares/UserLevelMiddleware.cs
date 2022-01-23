using System.Security.Claims;

namespace CoreIdentity.Middlewares
{
    public class UserLevelMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLevelMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var claims = new List<Claim>
            {
                new Claim("userlevel", "1")
            };

                var appIdentity = new ClaimsIdentity(claims);
                context.User.AddIdentity(appIdentity);
            }
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class UserLevelMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserLevel(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserLevelMiddleware>();
        }
    }
}
