using Business.Abstract;
using Entities.Concrete;

namespace WebAPI
{
    public class SystemAvailabilityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISystemAvailabilityTimeService _systemAvailabilityTimeService;

        public SystemAvailabilityMiddleware(RequestDelegate next, ISystemAvailabilityTimeService systemAvailabilityTimeService)
        {
            _next = next;
            _systemAvailabilityTimeService = systemAvailabilityTimeService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Always allow access to login endpoint
            if (httpContext.Request.Path.StartsWithSegments("/api/Auth/login"))
            {
                await _next(httpContext);
                return;
            }

            // Check if the user is an admin
            var isAdmin = httpContext.User.IsInRole("Admin");
            if (isAdmin)
            {
                await _next(httpContext);
                return;
            }

            // Get system availability time
            SystemAvailabilityTime systemAvailabilityTime = await _systemAvailabilityTimeService.GetSystemAvailabilityTimeAsync();
            var currentTime = DateTime.Now.TimeOfDay;

            // Check if the system is available
            if (IsSystemAvailable(currentTime, systemAvailabilityTime))
            {
                await _next(httpContext);
                return;
            }

            // System is not available
            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await httpContext.Response.WriteAsync("Backend is not available now!");
        }

        private bool IsSystemAvailable(TimeSpan currentTime, SystemAvailabilityTime systemAvailabilityTime)
        {
            var openTime = TimeSpan.FromHours(systemAvailabilityTime.OpenTime);
            var closeTime = TimeSpan.FromHours(systemAvailabilityTime.CloseTime);

            // Handle cases where the open time is after the close time (e.g., 22:00 to 06:00)
            if (closeTime < openTime)
            {
                return currentTime >= openTime || currentTime < closeTime;
            }

            // Normal case (e.g., 08:00 to 18:00)
            return currentTime >= openTime && currentTime < closeTime;
        }
    }

    public static class SystemAvailabilityMiddlewareExtensions
    {
        public static IApplicationBuilder UseSystemAvailabilityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SystemAvailabilityMiddleware>();
        }
    }
}