namespace Entity_framework
{
    public class LoggerMiddlewere
    {
        private readonly RequestDelegate next;

        public LoggerMiddlewere(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<LoggerMiddlewere> logger)
        {
            logger.LogInformation($"{httpContext.Request.Path}: {httpContext.Request.QueryString}");
             await next(httpContext);

        }
    }

    public static class ExtentionsClass
    {
        public static IApplicationBuilder UseLoggerMiddlere(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<LoggerMiddlewere>();
        }
    }
}
