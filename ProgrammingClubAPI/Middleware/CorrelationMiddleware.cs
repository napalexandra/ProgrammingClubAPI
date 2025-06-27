using System.Diagnostics;

namespace ProgrammingClubAPI.Middleware
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeader = "X-Correlation-ID";
        private readonly ILogger<CorrelationMiddleware> _logger;

        public CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault() ?? Guid.NewGuid().ToString();

            context.Items[CorrelationIdHeader] = correlationId;

            var startRequest = Stopwatch.StartNew();
            await _next(context);
            startRequest.Stop();

            _logger.LogInformation("Request {CorrelationId} completed in {ElapsedMilliseconds} ms", 
                correlationId, startRequest.ElapsedMilliseconds);
        }
    }
}
