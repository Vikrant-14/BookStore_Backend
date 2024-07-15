using RepositoryLayer.Context;
using RepositoryLayer.Utility;

namespace BookStore.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationDBContext userService, JwtValidation jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            await Console.Out.WriteLineAsync("userID jwt");
            if (userId != null)
            {
                await Console.Out.WriteLineAsync("inside");
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.Users.Find(userId.Value);
            }

            await _next(context);
        }
    }
}
