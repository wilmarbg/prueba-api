using System.Diagnostics;

namespace PruebaWebApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();
            var logMessage = $"{DateTime.Now}: {context.Request.Method} {context.Request.Path} executed in {stopwatch.ElapsedMilliseconds} ms";
            await File.AppendAllTextAsync("request_logs.txt", logMessage + Environment.NewLine);
        }
    }
}
